using System;
using System.Collections.Generic;
using Bunnypro.CommandLine.Commands;
using Newtonsoft.Json;

namespace Bunnypro.CommandLine.Examples.Simple
{
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