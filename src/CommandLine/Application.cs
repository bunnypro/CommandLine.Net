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
            if (!commandInfo.HasExecutionMethod) throw new Exception("Command cannot be executed.");
            
            // find match execution method or show help

            return command.ShowHelp(names);
        }
    }
}