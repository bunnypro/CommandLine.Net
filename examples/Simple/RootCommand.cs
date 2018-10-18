using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace Bunnypro.CommandLine.Examples.Simple
{
    public class RootCommand : Command
    {
        public override string Description => "Command of nothing";

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
            var args = new Dictionary<string, object>
            {
                {"option", options},
                {"arguments", arguments}
            };
            Console.WriteLine(JsonConvert.SerializeObject(args, Formatting.Indented));
            return 0;
        }
    }
}