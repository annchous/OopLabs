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
    }
}
