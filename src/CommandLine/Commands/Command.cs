using System;
using System.Collections.Generic;

namespace Bunnypro.CommandLine.Commands
{
    public abstract class Command
    {
        public virtual string Description { get; } = "No Command Description";
        public virtual IEnumerable<Option> Options { get; } = new Option[] { };

        public IDictionary<string, Command> Commands { get; } = new Dictionary<string, Command>();

        public void AddCommand(string name, Command command)
        {
            if (command.Equals(this))
                throw new Exception("Cannot add self");
            CommandValidator.Validate(command);
            Commands.Add(name, command);
        }

        public virtual int ShowHelp(IEnumerable<string> names)
        {
            var commandName = string.Join(" ", names);
            new CommandHelp(commandName, this).Print();
            return 1;
        }
    }
}
