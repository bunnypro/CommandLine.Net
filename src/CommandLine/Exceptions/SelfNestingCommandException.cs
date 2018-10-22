using System;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class SelfNestingCommandException : Exception
    {
        public Type Command { get; }

        public SelfNestingCommandException(Type command)
        {
            Command = command;
        }
    }
}
