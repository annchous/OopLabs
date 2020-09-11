using System;
using System.Collections.Generic;
using System.Text;
using IniParser.IniParserException;
using System.IO;

namespace IniParser
{
    class IniFile
    {
        public string Name { get; }
        public string Path { get; }
        public List<string> data;
        public IniFile(string path)
        {
            Name = System.IO.Path.GetFileName(path);
            Path = path;
            data = new List<string>();
        }

        public bool RightFormat() => GetFormat().Equals("ini");
        public string GetFormat() => Name.Substring(Name.IndexOf('.') + 1);
    }
}
