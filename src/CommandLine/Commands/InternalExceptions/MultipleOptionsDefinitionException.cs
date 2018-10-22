using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class MultipleOptionsDefinitionException : Exception
    {
        public ExecutableMethodInfo Method { get; }

        public MultipleOptionsDefinitionException(ExecutableMethodInfo method)
        {
            Method = method;
        }
    }
}
