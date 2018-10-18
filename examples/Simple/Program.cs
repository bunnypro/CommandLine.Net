using Bunnypro.CommandLine.Examples.Simple.Commands;

namespace Bunnypro.CommandLine.Examples.Simple
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var root = new RootCommand();
            root.AddCommand("new", new NewCommand());
            var app = new Application("abc", root);
            return app.Run(args);
        }
    }
}
