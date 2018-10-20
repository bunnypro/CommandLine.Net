using System;
using System.Collections.Generic;
using System.Linq;
using Bunnypro.CommandLine.Commands.Reflection;
using Bunnypro.CommandLine.Exceptions;

namespace Bunnypro.CommandLine.Commands
{
    internal sealed class UserInputExtractor
    {
        private readonly Command _command;

        public UserInputExtractor(Command command, IEnumerable<string> userInputArgs)
        {
            _command = command;
            Options = ExtractOptions(userInputArgs, out var arguments);
            Arguments = arguments.Select(v => new UserInputInfo(v)).ToList();
            var parameters = new List<UserInputInfo>();
            if (Options.Any())
                parameters.Add(new UserInputInfo(Options));

            parameters.AddRange(Arguments);
            Parameters = parameters;
        }

        public IDictionary<Option, object> Options { get; }
        public IEnumerable<UserInputInfo> Arguments { get; }
        public IEnumerable<UserInputInfo> Parameters { get; }
        public IEnumerable<UserInputInfo> ParametersWithOption
        {
            get
            {
                var parameters = Parameters.ToList();
                parameters.Insert(0, new UserInputInfo(Options));
                return parameters;
            }
        }

        public bool HasParameter => Parameters.Any();
        public bool HasOptions => Options.Any();
        public bool HasArguments => Parameters.Any(p => !p.IsOptions);

        private IDictionary<Option, object> ExtractOptions(IEnumerable<string> userInputArgs, out object[] arguments)
        {
            var args = userInputArgs.ToList();
            var options = new Dictionary<Option, object>();
            while (args.Any(a => a.StartsWith("-")))
            {
                Option option;
                var arg = args.First(a => a.StartsWith("-"));
                var pos = args.IndexOf(arg);
                args.Remove(arg);

                var optionName = arg.Substring(1);
                if (optionName.StartsWith("-"))
                {
                    optionName = optionName.Substring(1);
                    if (optionName.StartsWith("-"))
                        throw new OptionFormatException(arg);

                    option = _command.Options.FirstOrDefault(o => o.Name.Equals(optionName));

                    if (option == null)
                        throw new UnexpectedOptionException(arg);

                    if (option.IsAcceptValue && args.Count < pos)
                        throw new RequiredOptionValueException(option);

                    string value = null;
                    if (option.IsAcceptValue)
                    {
                        value = args[pos];
                        args.Remove(value);
                    }

                    AssignOptionValue(options, option, value);
                    continue;
                }

                if (!optionName.Any())
                    throw new OptionFormatException(arg);

                {
                    var lastOptionShortName = optionName.Last();
                    option = _command.Options.FirstOrDefault(o => lastOptionShortName.Equals(o.ShortName));

                    if (option == null)
                        throw new UnexpectedOptionException($"-{lastOptionShortName}");

                    if (option.IsAcceptValue && args.Count < pos)
                        throw new RequiredOptionValueException(option);

                    string value = null;
                    if (option.IsAcceptValue)
                    {
                        value = args[pos];
                        args.Remove(value);
                    }

                    AssignOptionValue(options, option, value);
                }

                foreach (var optionShortName in optionName.Take(optionName.Length - 1))
                {
                    option = _command.Options.FirstOrDefault(o => optionShortName.Equals(o.ShortName));
                    
                    if (option == null)
                        throw new UnexpectedOptionException($"-{optionShortName}");
                    
                    if (option.IsAcceptValue)
                        throw new RequiredOptionValueException(option);
                    
                    options.Add(option, null);
                }
            }

            arguments = args.Cast<object>().ToArray();
            return options;
        }

        private static void AssignOptionValue(IDictionary<Option, object> options, Option option, string value)
        {
            if (option.IsMultiValue)
            {
                if (options.ContainsKey(option))
                    ((List<object>) options[option]).Add(value);
                else
                    options.Add(option, new List<object>());
                return;
            }

            if (options.ContainsKey(option))
                throw new MultipleOptionException(option);

            options.Add(option, value);
        }

        public object[] FormatParametersFor(ExecutableMethodInfo method)
        {
            if (method == null)
                return new object[]{ };
            
            var methodParametersType = method.Parameters.Select(p => p.Type).ToList();
            
            return (
                method.IsAcceptOptions && !HasOptions ?
                    ParametersWithOption :
                    Parameters
            ).Select((param, i) =>
                param.Value is IConvertible ?
                    Convert.ChangeType(param.Value, methodParametersType[i]) :
                    param.Value
            ).ToArray();
        }
    }
}