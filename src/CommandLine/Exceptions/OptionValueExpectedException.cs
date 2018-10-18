using System;

namespace Bunnypro.CommandLine.Exceptions
{
    public class OptionValueExpectedException : Exception
    {
        public OptionValueExpectedException(Option name)
        {
            Name = name;
        }
        
        public Option Name { get; }
    }
}