namespace Tasker.Runner
{
    using System;

    internal static class CommandLine
    {
        public const string Service = "Service";
        public const string ServiceWithPrefix = "-Service";

        public const string Install = "Install";
        public const string Uninstall = "Uninstall";

        private const string Prefixes = "-/";
        private static readonly char[] PrefixesArray = Prefixes.ToCharArray();

        public static bool Contains(string[] args, string value)
        {
            return args != null && !string.IsNullOrEmpty(value)
              && Array.Exists(args, item => string.Equals(item.TrimStart(PrefixesArray), value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
