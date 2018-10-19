using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class MultipleCommandOptionDefinitionException : Exception
    {
        public ExecutableMethodInfo Method { get; }

        public MultipleCommandOptionDefinitionException(ExecutableMethodInfo method)
        {
            Method = method;
        }
    }
}