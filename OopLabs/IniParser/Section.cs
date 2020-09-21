using System;
using System.Collections.Generic;
using System.Text;

namespace IniParser
{
    class Section
    {
        public string Name { get; }
        public Section(string name)
        {
            Name = name;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Name.GetHashCode();
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Section section = obj as Section;

            if (section as Section == null) return false;

            return this.Name == section.Name;
        }
    }
}
