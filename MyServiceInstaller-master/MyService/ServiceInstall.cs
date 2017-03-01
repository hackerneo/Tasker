using System;
using System.Collections;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace MyService
{
  internal static class ServiceInstall
  {
    private const string AssemblyPathContextKey = "AssemblyPath";

    private const string ServiceNameParameter = "ServiceName";
    private const string DisplayNameParameter = "DisplayName";
    private const string DescriptionParameter = "Description";

    private const string StartTypeParameter = "StartType";
    private const string DelayedAutoStartParameter = "DelayedAutoStart";
    private const string DependedOnParameter = "DependedOn";

    private const string AccountParameter = "Account";
    private const string UserNameParameter = "UserName";
    private const string PasswordParameter = "Password";

    private const string CommandParameter = "Command";

    private static readonly char[] DependedOnDelimeters = { ';' };

    private const char Space = ' ';
    private const char DoubleQuote = '"';

    public static void UpdateCommandLine(InstallContext context) {
      if(context == null) {
        throw new ArgumentNullException("context");
      }//if

      var path = context.Parameters[AssemblyPathContextKey];
      var command = context.Parameters[CommandParameter];

      var builder = new StringBuilder();
      builder.Append(DoubleQuote).Append(path).Append(DoubleQuote);
      builder.Append(Space).Append(CommandLine.ServiceWithPrefix);
      if(!String.IsNullOrEmpty(command)) {
        builder.Append(Space).Append(command);
      }//if

      context.Parameters[AssemblyPathContextKey] = builder.ToString();

      const string LogMessageFormat1 = "Assemply path updated to: {0}";
      context.LogMessage(String.Format(LogMessageFormat1, builder));
    }

    #region Apply Parameters

    private static void ApplyParameter(InstallContext context, string parameter, Action<string> apply) {
      if(context == null) {
        throw new ArgumentNullException("context");
      } else if(apply == null) {
        throw new ArgumentNullException("apply");
      }//if

      var value = context.Parameters[parameter];
      if(!String.IsNullOrEmpty(value)) {
        apply(value);
        const string AppliedParameterFormat2 = "Applied parameter: {0} = \"{1}\"";
        context.LogMessage(String.Format(AppliedParameterFormat2, parameter, value));
      }//if
    }

    private static void ApplyParameter<T>(InstallContext context, string parameter, Func<string, T> parse, Action<T> apply) {
      if(context == null) {
        throw new ArgumentNullException("context");
      } else if(apply == null) {
        throw new ArgumentNullException("apply");
      } else if(parse == null) {
        throw new ArgumentNullException("parse");
      }//if

      ApplyParameter(context, parameter, value => apply(parse(value)));
    }

    private static void ApplyEnumParameter<T>(InstallContext context, string parameter, Action<T> apply) {
      if(context == null) {
        throw new ArgumentNullException("context");
      } else if(apply == null) {
        throw new ArgumentNullException("apply");
      }//if

      ApplyParameter(context, parameter, value => (T)Enum.Parse(typeof(T), value), apply);
    }

    public static void ApplyParameters(this ServiceInstaller installer, InstallContext context, bool install) {
      if(installer == null) {
        throw new ArgumentNullException("installer");
      }//if

      ApplyParameter(context, ServiceNameParameter, value => installer.ServiceName = value);

      if(install) {
        ApplyParameter(context, DisplayNameParameter, value => installer.DisplayName = value);
        ApplyParameter(context, DescriptionParameter, value => installer.Description = value);

        ApplyEnumParameter<ServiceStartMode>(context, StartTypeParameter, value => installer.StartType = value);
        ApplyParameter(context, DelayedAutoStartParameter, Boolean.Parse, value => installer.DelayedAutoStart = value);
        ApplyParameter(context, DependedOnParameter, value => installer.ServicesDependedOn = value.Split(DependedOnDelimeters, StringSplitOptions.RemoveEmptyEntries));
      }//if
    }

    public static void ApplyParameters(this ServiceProcessInstaller installer, InstallContext context, bool install) {
      if(installer == null) {
        throw new ArgumentNullException("installer");
      }//if

      if(install) {
        ApplyEnumParameter<ServiceAccount>(context, AccountParameter, value => installer.Account = value);
        if(installer.Account == ServiceAccount.User) {
          ApplyParameter(context, UserNameParameter, value => installer.Username = value);
          ApplyParameter(context, PasswordParameter, value => installer.Password = value);
        }//if
      }//if
    }

    #endregion Apply Parameters

    #region Install

    public static bool Install<T>() where T : Installer, new() {
      var path = Assembly.GetExecutingAssembly().Location;
      var commandLine = Environment.GetCommandLineArgs();
      return Install<T>(path, commandLine);
    }

    public static bool Install<T>(string[] commandLine) where T : Installer, new() {
      var path = Assembly.GetExecutingAssembly().Location;
      return Install<T>(path, commandLine);
    }

    public static bool Install<T>(string path, string[] commandLine) where T : Installer, new() {
      if(String.IsNullOrEmpty(path)) {
        throw new ArgumentNullException("path");
      }//if

      var install = CommandLine.Contains(commandLine, CommandLine.Install);
      var uninstall = CommandLine.Contains(commandLine, CommandLine.Uninstall);
      if(!install && !uninstall) {
        return false;
      } else if(install && uninstall) {
        throw new InvalidOperationException("install && uninstall");
      }//if

      using(var installer = new T()) {
        var transacted = new TransactedInstaller() {
          Context = new InstallContext(null, commandLine) {
            Parameters = { { AssemblyPathContextKey, path } },
          },

          Installers = { installer, }
        };

        using(transacted) {
          if(install) {
            var state = new Hashtable();
            transacted.Install(state);
          } else {
            transacted.Uninstall(null);
          }//if
        }//using
      }//using

      return true;
    }

    #endregion Install
  }
}
