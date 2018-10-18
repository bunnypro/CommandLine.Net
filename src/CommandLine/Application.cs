using System;
using System.Collections.Generic;
using System.Linq;
using Bunnypro.CommandLine.Exceptions;

namespace Bunnypro.CommandLine
{
    public class Application
    {
        private readonly string _name;
        private readonly Command _root;

        public Application(string name, Command root)
        {
            _name = name;
            _root = root;
        }

        public int Run(IEnumerable<string> args)
        {
            var names = new List<string> {_name};
            var command = _root;
            var filteredArgs = args.ToList();

            while (filteredArgs.Count > 0 && !filteredArgs[0].StartsWith("-"))
            {
                var child = filteredArgs[0];
                if (!command.Commands.ContainsKey(child))
                    break;

                names.Add(child);
                command = command.Commands[child];
                filteredArgs = filteredArgs.Skip(1).ToList();
            }

            Extract(command, filteredArgs, out var options, out var arguments);
            return command.Execute(names, options, arguments);
        }

        private static void Extract(Command command, IEnumerable<string> args, out IDictionary<Option, object> options, out IDictionary<Argument, object> arguments)
        {
            options = new Dictionary<Option, object>();
            arguments = new Dictionary<Argument, object>();
            var inputArgs = args.ToList();

            inputArgs = FilterOptions(command, options, inputArgs);
            FilterArguments(command, arguments, inputArgs);
        }

        private static List<string> FilterOptions(Command command, IDictionary<Option, object> options, List<string> inputArgs)
        {
            inputArgs = FilterFullNameOptions(command, options, inputArgs);
            inputArgs = FilterShortNameOptions(command, options, inputArgs);

            return inputArgs;
        }

        private static List<string> FilterFullNameOptions(Command command, IDictionary<Option, object> options, List<string> inputArgs)
        {
            while (inputArgs.Any(arg => arg.StartsWith("--")))
            {
                var arg = inputArgs.First(a => a.StartsWith("--"));
                var argIndex = inputArgs.IndexOf(arg);
                var optionName = arg.Substring(2);

                inputArgs.Remove(arg);

                if (optionName.StartsWith("-") || !command.Options.Any(o => optionName.Equals(o.Name)))
                    throw new UnknownOptionException(arg);

                var option = command.Options.First(o => optionName.Equals(o.Name));
                AssignOptionValue(options, inputArgs, option, argIndex);
            }

            return inputArgs;
        }

        private static List<string> FilterShortNameOptions(Command command, IDictionary<Option, object> options, List<string> inputArgs)
        {
            while (inputArgs.Any(arg => arg.StartsWith("-")))
            {
                var arg = inputArgs.First(a => a.StartsWith("-"));
                var argIndex = inputArgs.IndexOf(arg);
                var optionsName = arg.Substring(1);

                inputArgs.Remove(arg);

                Option option;
                for (var i = 0; i < optionsName.Length - 1; i++)
                {
                    if (!command.Options.Any(o => optionsName[i].Equals(o.ShortName)))
                        throw new UnknownOptionException($"-{optionsName[i]}");

                    option = command.Options.First(o => optionsName[i].Equals(o.ShortName));
                    if (option.AcceptValue)
                        throw new OptionValueExpectedException(option);
                    options.Add(option, null);
                }

                if (!command.Options.Any(o => optionsName.Last().Equals(o.ShortName)))
                    throw new UnknownOptionException($"-{optionsName.Last()}");

                option = command.Options.First(o => optionsName.Last().Equals(o.ShortName));
                AssignOptionValue(options, inputArgs, option, argIndex);
            }

            return inputArgs;
        }

        private static void AssignOptionValue(IDictionary<Option, object> options, List<string> inputArgs, Option option, int argIndex)
        {
            if (!option.MultiValue)
            {
                if (options.ContainsKey(option))
                    throw new UnexpectedMultiOptionException(option);

                string value = null;
                if (option.AcceptValue)
                {
                    if (inputArgs.Count < argIndex) throw new OptionValueExpectedException(option);
                    value = inputArgs[argIndex];
                    inputArgs.Remove(value);
                }

                options.Add(option, value);
                return;
            }

            if (inputArgs.Count < argIndex) throw new OptionValueExpectedException(option);

            List<string> values;
            if (!options.ContainsKey(option))
            {
                values = new List<string>();
                options.Add(option, values);
            }
            else
                values = (List<string>) options[option];

            var optionValue = inputArgs[argIndex];
            inputArgs.Remove(optionValue);
            values.Add(optionValue);
        }

        private static void FilterArguments(Command command, IDictionary<Argument, object> arguments, List<string> inputArgs)
        {
            var argCount = 0;
            var commandArguments = command.Arguments.ToList();
            while (inputArgs.Count > 0)
            {
                if (commandArguments.Count < argCount) throw new UnexpectedArgumentsException(inputArgs);
                var argument = commandArguments[argCount];
                if (argument.MultiValue)
                {
                    arguments.Add(argument, new List<string>(inputArgs));
                    inputArgs.Clear();
                    return;
                }

                arguments.Add(argument, inputArgs[0]);
                inputArgs.RemoveAt(0);
                argCount++;
            }
        }
    }
}