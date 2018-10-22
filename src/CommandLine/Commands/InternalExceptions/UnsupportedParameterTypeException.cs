using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class UnsupportedParameterTypeException : Exception
    {
        public ExecutableMethodParameterInfo Parameter { get; }

        public UnsupportedParameterTypeException(ExecutableMethodParameterInfo parameter)
        {
            Parameter = parameter;
        }
    }
}
