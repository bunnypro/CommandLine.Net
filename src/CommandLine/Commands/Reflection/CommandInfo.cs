using System.Collections.Generic;
using System.Linq;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    internal sealed class CommandInfo
    {
        public CommandInfo(Command command)
        {
            ExecutableMethods = command.GetType().GetMethods().Where(m => "Execute".Equals(m.Name))
                .Select(m => new ExecutableMethodInfo(command, m)).ToList();
        }

        public IEnumerable<ExecutableMethodInfo> ExecutableMethods { get; }

        public bool HasExecutableMethodConflict => ExecutableMethods.Any(m => ExecutableMethods.Any(m.Conflict));
        public bool HasExecutableMethod => ExecutableMethods.Any();
        public bool HasExecutableMethodWithOptions => ExecutableMethods.Any(m => m.IsAcceptOptions);

        public ExecutableMethodInfo FindExecutableMethodFor(InputExtractor input)
        {
            return ExecutableMethods.FirstOrDefault(m =>
                       m.HasParameter == input.HasParameter &&
                       m.IsAcceptOptions == input.HasOptions &&
                       m.Parameters.Count() == input.Parameters.Count()
                   ) ?? ExecutableMethods.FirstOrDefault(m =>
                       m.HasParameter == input.HasParameter &&
                       m.Arguments.Count() == input.Arguments.Count()
                   );
        }
    }
}