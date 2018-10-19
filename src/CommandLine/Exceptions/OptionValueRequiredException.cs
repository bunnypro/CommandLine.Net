using Bunnypro.CommandLine.Commands;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class OptionValueRequiredException : CommandRuntimeException
    {
        public Option Option { get; }

        public OptionValueRequiredException(Option option)
        {
            Option = option;
        }
    }
}