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
            Console.WriteLine(_command.Description);
            Console.WriteLine();
            
            if (_commandInfo.HasExecutableMethod || _command.Commands.Any())
            {
                PrintUsage();
                Console.WriteLine();
                
                if (_command.Commands.Any())
                {
                    PrintCommands();
                    Console.WriteLine();
                }

                if (_command.Options.Any())
                {
                    PrintOptions();
                    Console.WriteLine();
                }
            }
        }

        public void PrintUsage()
        {
            Console.WriteLine("Usage");

            foreach (var method in _commandInfo.ExecutableMethods)
            {
                var args = method.IsAcceptOptions ?
                    " [options]" :
                    "";
                
                args += method.Arguments.Aggregate("", (argument, parameter) =>
                {
                    argument += $" {{({parameter.TypeName}) {parameter.Name}}}";

                    return argument;
                });

                Console.WriteLine($"\t{_name}{args}");
            }

            if (_command.Commands.Any())
                Console.WriteLine($"\t{_name} [command]");
        }

        public void PrintCommands()
        {
            Console.WriteLine("Commands");

            foreach (var command in _command.Commands)
            {
                Console.WriteLine($"\t{command.Key}: {((Command) Activator.CreateInstance(command.Value)).Description}");
            }
        }

        public void PrintOptions()
        {
            Console.WriteLine("Options");
            
            foreach (var option in _command.Options)
            {
                var value = option.IsMultiValue ? " Accept Multi Option Value" : option.IsAcceptValue ? " Accept Value" : "";
                Console.Write($"\t-{option.ShortName}");
                Console.WriteLine($"  --{option.Name}{value}");
                Console.WriteLine($"\t      {option.Description}");
            }
        }
    }
}