using System.Collections.Generic;

namespace Bunnypro.CommandLine.Commands
{
    public abstract class Command
    {
        public virtual string Description => "No Command Description";
        public virtual IEnumerable<Option> Options => new Option[] { };
        public virtual CommandDictionary Commands => new CommandDictionary();
    }
}
