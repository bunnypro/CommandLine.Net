using System;
using System.Collections.Generic;

namespace Bunnypro.CommandLine.Exceptions
{
    public class UnexpectedArgumentsException : Exception
    {
        public UnexpectedArgumentsException(IEnumerable<string> arguments)
        {
            Arguments = arguments;
        }

        public IEnumerable<string> Arguments { get; }
    }
}