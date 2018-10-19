using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class ExecutionMethodParameterOrderException : Exception
    {
        public ExecutableMethodInfo Method { get; }

        public ExecutionMethodParameterOrderException(ExecutableMethodInfo method)
        {
            Method = method;
        }
    }
}