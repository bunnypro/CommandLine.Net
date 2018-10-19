using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    internal sealed class ExecutableMethodInfo
    {
        public ExecutableMethodInfo(Command command, MethodInfo method)
        {
            Command = command;
            ExecutableMethod = method;
            Parameters = method.GetParameters().Select(p => new ExecutableMethodParameterInfo(this, p)).ToList();
        }
        
        public Command Command { get; }
        public MethodInfo ExecutableMethod { get; }

        public IEnumerable<ExecutableMethodParameterInfo> Parameters { get; }
        public IEnumerable<ExecutableMethodParameterInfo> Arguments => Parameters.Where(p => p.IsArgument);

        public bool HasParameter => Parameters.Any();
        public bool HasArgument => Arguments.Any();
        public bool IsAcceptOptions => Parameters.Any(p => p.IsOptions);

        public bool Conflict(ExecutableMethodInfo other)
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