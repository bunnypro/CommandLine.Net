using System.Linq;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands
{
    internal sealed class ExecutableMethodFinder
    {
        private readonly UserInputExtractor _input;
        private readonly CommandInfo _commandInfo;

        public ExecutableMethodFinder(Command command, UserInputExtractor input)
        {
            _input = input;
            _commandInfo = new CommandInfo(command);
        }

        public ExecutableMethodInfo ExecutableMethod =>
            _commandInfo.ExecutableMethods.FirstOrDefault(m =>
                m.HasParameter == _input.HasParameter &&
                m.IsAcceptOptions == _input.HasOptions &&
                m.Parameters.Count() == _input.Parameters.Count()
            ) ?? _commandInfo.ExecutableMethods.FirstOrDefault(m =>
                m.HasParameter == _input.HasParameter &&
                m.Arguments.Count() == _input.Arguments.Count()
            );
    }
}