using System;
using System.Collections.Generic;
using System.Linq;
using Bunnypro.CommandLine.Commands;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine
{
    public sealed class Application<T> where T : Command, new()
    {
        private readonly string _name;

        public Application(string name)
        {
            _name = name;
        }

        public int Run(IEnumerable<string> args)
        {
            var names = new List<string> {_name};
            var command = (Command) Activator.CreateInstance<T>();
            var commandArgs = args.ToList();

            while (commandArgs.Any() && !commandArgs[0].StartsWith("-"))
            {
                var child = commandArgs[0];
                if (!command.Commands.ContainsKey(child))
                    break;

                names.Add(child);
                command = (Command) Activator.CreateInstance(command.Commands[child]);
                commandArgs = commandArgs.Skip(1).ToList();
            }

            CommandValidator.Validate(command);

            try
            {
                var commandInfo = new CommandInfo(command);
                var input = new InputExtractor(command, commandArgs);
                var method = commandInfo.FindExecutableMethodFor(input);

                if (method != null)
                {
                    var parameters = input.FormatParametersFor(method);
                    return method.Invoke(parameters);
                }

                Console.WriteLine("Invalid Command Usage Format.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Argument Type.");
            }

            var commandName = string.Join(" ", names);
            new CommandHelp(commandName, command).Print();
            return 1;
        }
    }
}
