using System;
using System.Collections;
using System.Collections.Generic;

namespace Bunnypro.CommandLine.Commands
{
    public class CommandDictionary : IEnumerable<KeyValuePair<string, Type>>
    {
        private readonly Command _parent;
        private readonly IDictionary<string, Type> _commands = new Dictionary<string, Type>();

        public CommandDictionary(Command parent)
        {
            _parent = parent;
        }

        public void Add(string name, Type command)
        {
            if (_parent == null)
                return;

            _commands.Add(name, command);
        }

        public bool ContainsKey(string key)
        {
            return _commands.ContainsKey(key);
        }

        public Type this[string key] => _commands[key];

        public IEnumerator<KeyValuePair<string, Type>> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
