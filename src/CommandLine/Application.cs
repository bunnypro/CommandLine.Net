using System;
using System.Collections.Generic;
using System.Linq;
using Bunnypro.CommandLine.Commands;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine
{
    public sealed class Application
    {
        private readonly string _name;
        private readonly Command _root;

        public Application(string name, Command root)
        {
            CommandValidator.Validate(root);
            _name = name;
            _root = root;
        }

        public int Run(IEnumerable<string> args)
        {
            var names = new List<string> {_name};
            var command = _root;
            var commandArgs = args.ToList();

            while (commandArgs.Any() && !commandArgs[0].StartsWith("-"))
            {
                var child = commandArgs[0];
                if (!command.Commands.ContainsKey(child))
                    break;

                names.Add(child);
                command = command.Commands[child];
                commandArgs = commandArgs.Skip(1).ToList();
            }

            var commandInfo = new CommandInfo(command);

            if (commandArgs.Any())
            {
                try
                {
                    var method = commandInfo.FindMatchExecutableMethodInfo(commandArgs, out var parameters);
                    
                    if (method != null)
                    {
                        return method.Invoke(parameters);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Argument Type.");
                }
            }

            var commandName = string.Join(" ", names);
            new CommandHelp(commandName, command).Print();
            return 1;
        }
    }
}