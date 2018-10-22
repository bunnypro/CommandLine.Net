using Bunnypro.CommandLine.Commands;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class MultipleOptionException : CommandRuntimeException
    {
        public Option Option { get; }

        public MultipleOptionException(Option option)
        {
            Option = option;
        }
    }
}
