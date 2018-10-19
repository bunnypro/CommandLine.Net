using System;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class OptionFormatException : Exception
    {
        public string Name { get; }

        public OptionFormatException(string name)
        {
            Name = name;
        }
    }
}