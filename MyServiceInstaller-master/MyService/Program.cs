using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace MyService
{
  internal static class Program
  {
    private const int SuccessExitCode = 0;
    private const int FailedExitCode = 1;

    private static int Main(string[] args) {
      try {
        if(ServiceInstall.Install<ProjectInstaller>(args)) {
          return SuccessExitCode;
        } else if(CommandLine.Contains(args, CommandLine.Service)) {
          return StartService(args);
        } else {
          return StartConsole(args);
        }//if
      } catch(Exception ex) {
        Console.WriteLine(ex);
        throw;
      }//try
    }

    private static int StartService(string[] args) {
      using(var service = new MyService()) {
        //Debugger.Break();
        ServiceBase.Run(service);
        return service.ExitCode;
      }//using
    }

    private static int StartConsole(string[] args) {
      Console.Write("Service started. Press <Enter> for stop service and exit.");
      Console.ReadLine();
      return SuccessExitCode;
    }
  }
}
