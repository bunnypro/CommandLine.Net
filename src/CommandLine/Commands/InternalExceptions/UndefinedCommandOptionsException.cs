using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class UndefinedCommandOptionsException : Exception
    {
        public ExecutableMethodInfo Method { get; }

        public UndefinedCommandOptionsException(ExecutableMethodInfo method)
        {
            Method = method;
        }
    }
}
