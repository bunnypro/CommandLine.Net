using System;
using System.Collections.Generic;
using System.Linq;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    internal sealed class CommandInfo
    {
        private readonly Command _command;

        public CommandInfo(Command command)
        {
            _command = command;
            ExecutableMethods = command.GetType().GetMethods().Where(m => "Execute".Equals(m.Name))
                .Select(m => new ExecutableMethodInfo(command, m)).ToList();
        }

        public IEnumerable<ExecutableMethodInfo> ExecutableMethods { get; }

        public bool HasExecutableMethodConflict => ExecutableMethods.Any(m => ExecutableMethods.Any(m.Conflict));
        public bool HasExecutableMethod => ExecutableMethods.Any();

        public ExecutableMethodInfo FindMatchExecutableMethodInfo(IEnumerable<string> commandArgs, out object[] parameters)
        {
            var input = new UserInputExtractor(_command, commandArgs);
            var executableMethod = ExecutableMethods.FirstOrDefault(m =>
                                       m.HasParameter == input.HasParameter &&
                                       m.IsAcceptOptions == input.HasOptions &&
                                       m.Parameters.Count() == input.Parameters.Count()
                                   ) ?? ExecutableMethods.FirstOrDefault(m =>
                                       m.HasParameter == input.HasParameter &&
                                       m.Arguments.Count() == input.Arguments.Count()
                                   );

            parameters = new object[] { };
            if (executableMethod == null)
                return null;

            var methodParametersType = executableMethod.Parameters.Select(p => p.Type).ToList();

            parameters = (
                executableMethod.IsAcceptOptions && !input.HasOptions ?
                    input.ParametersWithOption :
                    input.Parameters
            ).Select((param, i) =>
                param.Value is IConvertible ?
                    Convert.ChangeType(param.Value, methodParametersType[i]) :
                    param.Value
            ).ToArray();

            return executableMethod;
        }
    }
}