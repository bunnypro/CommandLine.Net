using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    internal sealed class ExecutionMethodInfo
    {
        public ExecutionMethodInfo(Command command, MethodInfo method)
        {
            Command = command;
            Parameters = method.GetParameters().Select(p => new ExecutionMethodParameterInfo(this, p)).ToList();
        }
        
        public Command Command { get; }

        public IEnumerable<ExecutionMethodParameterInfo> Parameters { get; }
        public IEnumerable<ExecutionMethodParameterInfo> Arguments => Parameters.Where(p => p.IsArgument);

        public bool HasParameter => Parameters.Any();
        public bool HasArgument => Arguments.Any();
        public bool IsAcceptOptions => Parameters.Any(p => p.IsOptions);

        public bool Conflict(ExecutionMethodInfo other)
        {
            if (other == null)
                return false;

            if (Equals(other))
                return false;
            
            if (IsAcceptOptions && !other.IsAcceptOptions || !IsAcceptOptions && other.IsAcceptOptions)
                return false;

            return Parameters.Count() == other.Parameters.Count();
        }
    }
}