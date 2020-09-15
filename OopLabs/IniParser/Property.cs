using System;
using System.Collections.Generic;
using System.Text;

namespace IniParser
{
    class Property
    {
        public string Name { get; }
        public string Value { get; }
        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
