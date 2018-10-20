using System;
using Bunnypro.CommandLine.Commands;

namespace Bunnypro.CommandLine.Exceptions
{
    public sealed class InvalidCommandTypeException : Exception
    {
        public InvalidCommandTypeException(Type command) : base($"Type of {command.FullName} is not instance of {typeof(Command).FullName}")
        {
        }
    }
}