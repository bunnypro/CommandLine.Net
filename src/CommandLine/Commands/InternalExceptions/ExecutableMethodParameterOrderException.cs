using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class ExecutableMethodParameterOrderException : Exception
    {
        public ExecutableMethodInfo Method { get; }

        public ExecutableMethodParameterOrderException(ExecutableMethodInfo method)
        {
            Method = method;
        }
    }
}
