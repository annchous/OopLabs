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
        public List<string> Data;
        public IniFile(string path)
        {
            Name = System.IO.Path.GetFileName(path);
            Path = path;
            Data = new List<string>();
        }

        public bool RightFormat() => GetFormat().Equals("ini");
        public string GetFormat() => Name.Substring(Name.LastIndexOf('.') + 1);

        public void ReadData()
        {
            if (!File.Exists(Path))
                throw new FileNotFound("This file was not found!");
            if (!RightFormat())
                throw new WrongFileFormat("Wrong file format! It should be '.ini'!");
            StreamReader sr = new StreamReader(Path);
            while (!sr.EndOfStream)
                Data.Add(sr.ReadLine());
        }
    }
}
