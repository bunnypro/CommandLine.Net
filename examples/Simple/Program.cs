using System;
using System.Collections.Generic;
using Bunnypro.CommandLine.Commands;

namespace Bunnypro.CommandLine.Examples.Simple
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var root = new RootCommand();
            var app = new Application("abc", root);
            root.AddCommand("new", new NewCommand());
            return app.Run(args);
        }

        public class RootCommand : Command
        {
            public override IEnumerable<Option> Options => new[]
            {
                new Option("image") { ShortName = 'i', Description = "Image File", MultiValue = true},
                new Option("name") { ShortName = 'n', Description = "Project Name", AcceptValue = true}, 
            };
            
            public int Execute(IDictionary<Option, object> options, object argument, int abc)
            {
                Console.WriteLine(argument);
                return 0;
            }

            public int Execute(IDictionary<Option, object> options, string first, int second, object third)
            {
                return 0;
            }
        }

        public class NewCommand : Command
        {
            public override IEnumerable<Option> Options => new[]
            {
                new Option("image") { ShortName = 'i', Description = "Image File", MultiValue = true},
                new Option("name") { ShortName = 'n', Description = "Project Name", AcceptValue = true}, 
            };
            
//            public int Execute(IDictionary<Option, object> options, object argument, int second)
//            {
//                Console.WriteLine(argument);
//                return 0;
//            }
//
            public int Execute(string first, object third)
            {
                return 0;
            }
        }
    }
}
