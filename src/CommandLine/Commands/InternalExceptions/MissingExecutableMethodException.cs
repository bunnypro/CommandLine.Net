using System;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class MissingExecutableMethodException : Exception
    {
        public Command Command { get; }

        public MissingExecutableMethodException(Command command)
        {
            Command = command;
        }
    }
}