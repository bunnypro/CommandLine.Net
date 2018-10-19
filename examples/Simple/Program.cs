using System;
using System.Collections.Generic;
using Bunnypro.CommandLine.Commands;
using Newtonsoft.Json;

namespace Bunnypro.CommandLine.Examples.Simple
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var root = new RootCommand();
            var app = new Application("abc", root);
            return app.Run(args);
        }
    }

    internal class RootCommand : Command
    {
        public override IEnumerable<Option> Options => new[]
        {
            new Option("name") {ShortName = 'n'}
        };

        public void Execute(IDictionary<Option, object> options, string first, int second)
        {
            Console.WriteLine("With Option");
            Console.WriteLine(first);
            Console.WriteLine(second);
        }

        public void Execute(string first, object second)
        {
            Console.WriteLine(first);
            Console.WriteLine(second);
        }

        public void Execute(IDictionary<Option, object> options)
        {
            Console.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));
        }
    }
}