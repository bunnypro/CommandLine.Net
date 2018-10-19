using Bunnypro.CommandLine.Commands;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class RequiredOptionValueException : CommandRuntimeException
    {
        public Option Option { get; }

        public RequiredOptionValueException(Option option)
        {
            Option = option;
        }
    }
}