using System;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class ExecutionMethodConflictException : Exception
    {
        public Command Command { get; }

        public ExecutionMethodConflictException(Command command)
        {
            Command = command;
        }
    }
}