using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Bunnypro.CommandLine
{
    public abstract class Command
    {
        public virtual string Description { get; } = "No Command Description";
        public IDictionary<string, Command> Commands { get; } = new Dictionary<string, Command>();
        public virtual ImmutableHashSet<Option> Options { get; } = new List<Option>().ToImmutableHashSet();
        public virtual ImmutableHashSet<Argument> Arguments { get; } = new List<Argument>().ToImmutableHashSet();

        public void AddCommand(string name, Command command)
        {
            Commands.Add(name, command);
        }

        public int Execute(IEnumerable<string> names, IDictionary<Option, object> options, IDictionary<Argument, object> arguments)
        {
            if (options.Count != 0 || arguments.Count != 0) return ExecuteCommand(options, arguments);
            ShowHelp(names);
            return 1;
        }

        protected abstract int ExecuteCommand(IDictionary<Option, object> options, IDictionary<Argument, object> arguments);

        private void ShowHelp(IEnumerable<string> names)
        {
            var command = string.Join(" ", names);
            Console.WriteLine($"Show help for command: {command}");
            Console.WriteLine($"\t{Description}");
            var usage = "";

            if (Commands.Count > 0)
                usage += " [command]";
            
            if (Options.Count > 0)
                usage += " [options]";

            if (Arguments.Count > 0)
            {
                usage += Arguments.Aggregate("", (str, a) =>
                {
                    str += $" {{{a.Name}}}";
                    return str;
                });
            }
            
            Console.WriteLine($"\nUsage: {command}{usage}");

            if (Commands.Count > 0)
            {
                Console.WriteLine("\nCommand:");
                foreach (var c in Commands)
                {
                    Console.WriteLine($"\t{c.Key}: {c.Value.Description}");
                }
            }

            if (Arguments.Count > 0)
            {
                Console.WriteLine("\nArguments:");
                foreach (var argument in Arguments)
                {
                    Console.WriteLine($"\t{argument.Name}: {argument.Description}");
                }
            }

            if (Options.Count > 0)
            {
                Console.WriteLine("\nOptions:");
                foreach (var option in Options)
                {
                    Console.Write("\t");
                    Console.Write(option.ShortName == null ? "  " : $"-{option.ShortName}");
                    Console.WriteLine($"  --{option.Name}: {option.Description}");
                }
            }
        }
    }
}
