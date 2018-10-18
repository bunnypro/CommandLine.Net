using System;

namespace Bunnypro.CommandLine
{
    public sealed class Argument : IEquatable<Argument>
    {
        public Argument(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
        public string Description { get; set; } = "No Description";
        public bool MultiValue { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Argument other)
        {
            return other != null && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Argument other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name != null ?
                Name.GetHashCode() :
                0;
        }
    }
}