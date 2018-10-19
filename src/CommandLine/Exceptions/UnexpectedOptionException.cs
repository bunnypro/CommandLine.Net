namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class UnexpectedOptionException : CommandRuntimeException
    {
        public string Name { get; }

        public UnexpectedOptionException(string name)
        {
            Name = name;
        }
    }
}