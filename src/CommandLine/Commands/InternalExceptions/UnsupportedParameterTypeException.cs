using System;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class UnsupportedParameterTypeException : Exception
    {
        public ExecutionMethodParameterInfo Parameter { get; }

        public UnsupportedParameterTypeException(ExecutionMethodParameterInfo parameter)
        {
            Parameter = parameter;
        }
    }
}