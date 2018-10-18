using System.Collections.Generic;
using System.Linq;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    internal sealed class CommandInfo
    {
        public CommandInfo(Command command)
        {
            ExecutionMethods = command.GetType().GetMethods().Where(m => "Execute".Equals(m.Name))
                .Select(m => new ExecutionMethodInfo(command, m)).ToList();
        }

        public IEnumerable<ExecutionMethodInfo> ExecutionMethods { get; }
        
        public bool HasExecutionMethodConflict => ExecutionMethods.Any(m => ExecutionMethods.Any(m.Conflict));
        public bool HasExecutionMethod => ExecutionMethods.Any();
    }
}