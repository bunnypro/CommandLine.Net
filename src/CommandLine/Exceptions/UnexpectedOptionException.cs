using System;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class UnexpectedOptionException : Exception
    {
        public string Name { get; }

        public UnexpectedOptionException(string name)
        {
            Name = name;
        }
    }
}