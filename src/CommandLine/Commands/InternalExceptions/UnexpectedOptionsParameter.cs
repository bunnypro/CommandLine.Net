using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class UnexpectedOptionsParameter : Exception
    {
        public ExecutableMethodInfo Method { get; }

        public UnexpectedOptionsParameter(ExecutableMethodInfo method)
        {
            Method = method;
        }
    }
}