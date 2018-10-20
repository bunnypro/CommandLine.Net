namespace Bunnypro.CommandLine.Examples.Simple
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return new Application<RootCommand>("abc").Run(args);
        }
    }
}