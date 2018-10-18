using System;

namespace Bunnypro.CommandLine.Exceptions
{
    public class UnknownOptionException : Exception
    {
        public UnknownOptionException(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
    }
}