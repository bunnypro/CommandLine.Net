using System.Collections.Generic;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    public class UserInputInfo
    {
        public UserInputInfo(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public bool IsOptions => Value.GetType() == typeof(IDictionary<Option, object>);
        public bool IsArgument => !IsOptions;
    }
}
