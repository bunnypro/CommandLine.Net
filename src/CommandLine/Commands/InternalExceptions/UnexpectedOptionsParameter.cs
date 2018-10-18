using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class UnexpectedOptionsParameter : Exception
    {
        public ExecutionMethodInfo Method { get; }

        public UnexpectedOptionsParameter(ExecutionMethodInfo method)
        {
            Method = method;
        }
    }
}