using System;

namespace Bunnypro.CommandLine.Commands.InternalExceptions
{
    internal sealed class MissingExecutionMethodException : Exception
    {
        public Command Command { get; }

        public MissingExecutionMethodException(Command command)
        {
            Command = command;
        }
    }
}