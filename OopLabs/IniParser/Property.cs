using System;
using System.Collections.Generic;
using System.Text;

namespace IniParser
{
    class Property
    {
        public string Name { get; }
        public object Value { get; }
        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
