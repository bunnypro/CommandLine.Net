using System;

namespace Bunnypro.CommandLine
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
        public bool AcceptValue { get; set; }

        public bool MultiValue
        {
            get => _multiValue && AcceptValue;
            set
            {
                _multiValue = value;

                if (_multiValue) AcceptValue = true;
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