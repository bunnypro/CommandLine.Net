using System;
using System.Linq;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands
{
    public sealed class CommandHelp
    {
        private readonly string _name;
        private readonly Command _command;
        private readonly CommandInfo _commandInfo;

        public CommandHelp(string name, Command command)
        {
            _name = name;
            _command = command;
            _commandInfo = new CommandInfo(command);
        }

        public void Print()
        {
            Console.WriteLine($"Show help for: {_name}");
            Console.WriteLine($"\tDescription: {_command.Description}");
            Console.WriteLine();
            PrintUsage();
            Console.WriteLine();
            PrintCommands();
            Console.WriteLine();
            PrintOptions();
        }

        public void PrintUsage()
        {
            Console.WriteLine("Usage");

            foreach (var method in _commandInfo.ExecutionMethods)
            {
                var args = method.IsAcceptOptions ?
                    " [options]" :
                    "";
                
                args += method.Arguments.Aggregate("", (argument, parameter) =>
                {
                    argument += $" {{{parameter.Name}:{parameter.TypeName}}}";

                    return argument;
                });

                Console.WriteLine($"\t{_name}{args}");
            }

            if (_command.Commands.Any())
                Console.WriteLine($"\t{_name} [command]");
        }

        public void PrintCommands()
        {
            if (!_command.Commands.Any()) return;

            Console.WriteLine("Commands");

            foreach (var command in _command.Commands)
            {
                Console.WriteLine($"\t{command.Key}: {command.Value.Description}");
            }
        }

        public void PrintOptions()
        {
            if (!_command.Options.Any()) return;
            
            Console.WriteLine("Options");
            
            foreach (var option in _command.Options)
            {
                var value = option.MultiValue ? ": Accept Multi Option Value" : option.AcceptValue ? ": Accept Value" : "";
                Console.Write($"\t-{option.ShortName}");
                Console.WriteLine($"  --{option.Name}{value}");
                Console.WriteLine($"\t    {option.Description}");
            }
        }
    }
}