namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class OptionFormatException : CommandRuntimeException
    {
        public string Name { get; }

        public OptionFormatException(string name)
        {
            Name = name;
        }
    }
}
