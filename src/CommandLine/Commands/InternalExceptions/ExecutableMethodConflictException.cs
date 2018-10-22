using System;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class ExecutableMethodConflictException : Exception
    {
        public Command Command { get; }

        public ExecutableMethodConflictException(Command command)
        {
            Command = command;
        }
    }
}
