namespace Tasker.Runner
{
    using System;
    using System.Collections;
    using System.Configuration.Install;
    using System.Reflection;
    using System.ServiceProcess;
    using System.Text;

    internal static class ServiceInstall
    {
        private const char Space = ' ';
        private const char DoubleQuote = '"';

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

        public static void UpdateCommandLine(InstallContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var path = context.Parameters[AssemblyPathContextKey];
            var command = context.Parameters[CommandParameter];

            var builder = new StringBuilder();
            builder.Append(DoubleQuote).Append(path).Append(DoubleQuote);
            builder.Append(Space).Append(CommandLine.ServiceWithPrefix);
            if (!string.IsNullOrEmpty(command))
            {
                builder.Append(Space).Append(command);
            }

            context.Parameters[AssemblyPathContextKey] = builder.ToString();

            const string LogMessageFormat1 = "Assemply path updated to: {0}";
            context.LogMessage(string.Format(LogMessageFormat1, builder));
        }

        public static void ApplyParameters(this ServiceInstaller installer, InstallContext context, bool install)
        {
            if (installer == null)
            {
                throw new ArgumentNullException("installer");
            }

            ApplyParameter(context, ServiceNameParameter, value => installer.ServiceName = value);

            if (install)
            {
                ApplyParameter(context, DisplayNameParameter, value => installer.DisplayName = value);
                ApplyParameter(context, DescriptionParameter, value => installer.Description = value);

                ApplyEnumParameter<ServiceStartMode>(context, StartTypeParameter, value => installer.StartType = value);
                ApplyParameter(context, DelayedAutoStartParameter, bool.Parse, value => installer.DelayedAutoStart = value);
                ApplyParameter(context, DependedOnParameter, value => installer.ServicesDependedOn = value.Split(DependedOnDelimeters, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        public static void ApplyParameters(this ServiceProcessInstaller installer, InstallContext context, bool install)
        {
            if (installer == null)
            {
                throw new ArgumentNullException("installer");
            }

            if (install)
            {
                ApplyEnumParameter<ServiceAccount>(context, AccountParameter, value => installer.Account = value);
                if (installer.Account == ServiceAccount.User)
                {
                    ApplyParameter(context, UserNameParameter, value => installer.Username = value);
                    ApplyParameter(context, PasswordParameter, value => installer.Password = value);
                }
            }
        }
       
        public static bool Install<T>() where T : Installer, new()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var commandLine = Environment.GetCommandLineArgs();
            return Install<T>(path, commandLine);
        }

        public static bool Install<T>(string[] commandLine) where T : Installer, new()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            return Install<T>(path, commandLine);
        }

        public static bool Install<T>(string path, string[] commandLine) where T : Installer, new()
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            var install = CommandLine.Contains(commandLine, CommandLine.Install);
            var uninstall = CommandLine.Contains(commandLine, CommandLine.Uninstall);

            if (!install && !uninstall)
            {
                return false;
            }

            if (install && uninstall)
            {
                throw new InvalidOperationException("install && uninstall");
            }

            using (var installer = new T())
            {
                var transacted = new TransactedInstaller()
                {
                    Context = new InstallContext(null, commandLine)
                    {
                        Parameters = { { AssemblyPathContextKey, path } }
                    },

                    Installers = { installer }
                };

                using (transacted)
                {
                    if (install)
                    {
                        var state = new Hashtable();
                        transacted.Install(state);
                    }
                    else
                    {
                        transacted.Uninstall(null);
                    }
                }
            }

            return true;
        }

        private static void ApplyParameter(InstallContext context, string parameter, Action<string> apply)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (apply == null)
            {
                throw new ArgumentNullException("apply");
            }

            var value = context.Parameters[parameter];
            if (!string.IsNullOrEmpty(value))
            {
                apply(value);
                const string AppliedParameterFormat2 = "Applied parameter: {0} = \"{1}\"";
                context.LogMessage(string.Format(AppliedParameterFormat2, parameter, value));
            }
        }

        private static void ApplyParameter<T>(InstallContext context, string parameter, Func<string, T> parse, Action<T> apply)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (apply == null)
            {
                throw new ArgumentNullException("apply");
            }

            if (parse == null)
            {
                throw new ArgumentNullException("parse");
            }

            ApplyParameter(context, parameter, value => apply(parse(value)));
        }

        private static void ApplyEnumParameter<T>(InstallContext context, string parameter, Action<T> apply)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (apply == null)
            {
                throw new ArgumentNullException("apply");
            }

            ApplyParameter(context, parameter, value => (T)Enum.Parse(typeof(T), value), apply);
        }
    }
}
