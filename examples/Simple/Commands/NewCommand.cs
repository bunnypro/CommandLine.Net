using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace Bunnypro.CommandLine.Examples.Simple.Commands
{
    public class NewCommand : Command
    {
        public override ImmutableHashSet<Option> Options => new[]
        {
            new Option("help") {ShortName = 'h', Description = "Shows helps"},
            new Option("image") {Description = "Define Image", MultiValue = true}
        }.ToImmutableHashSet();

        public override ImmutableHashSet<Argument> Arguments => new[]
        {
            new Argument("name") {Description = "Name"},
            new Argument("image") {Description = "Image name", MultiValue = true}
        }.ToImmutableHashSet();
        
        protected override int ExecuteCommand(IDictionary<Option, object> options, IDictionary<Argument, object> arguments)
        {
            Console.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(arguments, Formatting.Indented));
            return 0;
        }
    }
}