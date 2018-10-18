using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class MultipleCommandOptionDefinitionException : Exception
    {
        public ExecutionMethodInfo Method { get; }

        public MultipleCommandOptionDefinitionException(ExecutionMethodInfo method)
        {
            Method = method;
        }
    }
}