using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class ExecutionMethodParameterOrderException : Exception
    {
        public ExecutionMethodInfo Method { get; }

        public ExecutionMethodParameterOrderException(ExecutionMethodInfo method)
        {
            Method = method;
        }
    }
}