using System;
using Bunnypro.CommandLine.Commands;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class OptionValueRequiredException : Exception
    {
        public Option Option { get; }

        public OptionValueRequiredException(Option option)
        {
            Option = option;
        }
    }
}