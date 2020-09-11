using System;
using System.Collections.Generic;
using System.Text;
using IniParser.IniParserException;

namespace IniParser
{
    class IniFile
    {
        public string Name { get; }
        public string Path { get; }
        public IniFile(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public bool CheckFormat() => GetFormat().Equals("ini");
        public string GetFormat() => Name.Substring(Name.IndexOf('.') + 1);
    }
}
