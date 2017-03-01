using System;

namespace MyService
{
  internal static class CommandLine
  {
    private const string Prefixes = "-/";
    private static readonly char[] PrefixesArray = Prefixes.ToCharArray();

    public const string Service = "Service";
    public const string ServiceWithPrefix = "-Service";

    public const string Install = "Install";
    public const string Uninstall = "Uninstall";

    public static bool Contains(string[] args, string value) {
      return args != null && !String.IsNullOrEmpty(value)
        && Array.Exists(args, item => String.Equals(item.TrimStart(PrefixesArray), value, StringComparison.OrdinalIgnoreCase));
    }
  }
}
