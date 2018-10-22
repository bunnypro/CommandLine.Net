using System;

namespace Bunnypro.CommandLine.Commands
{
    public sealed class Option : IEquatable<Option>
    {
        private bool _multiValue;

        public Option(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public char? ShortName { get; set; }
        public string Description { get; set; } = "No Description";

        public bool IsAcceptValue { get; set; }

        public bool IsMultiValue
        {
            get => _multiValue && IsAcceptValue;
            set
            {
                _multiValue = value;
                if (value) IsAcceptValue = true;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Option other)
        {
            return other != null && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Option other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name != null ?
                Name.GetHashCode() :
                0;
        }
    }
}
