using System;

namespace Bunnypro.CommandLine.Exceptions
{
    public class UnexpectedMultiOptionException : Exception
    {
        public UnexpectedMultiOptionException(Option option)
        {
            Option = option;
        }

        public Option Option { get; }
    }
}