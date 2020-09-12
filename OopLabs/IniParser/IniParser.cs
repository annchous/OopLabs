using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using IniParser.IniParserException;

namespace IniParser
{
    class IniParser
    {
        public Parser Parser;
        public IniData Data;

        public IniParser()
        {
            Parser = new Parser();
            Data = new IniData();
        }
        public IniParser(string path)
        {
            Parser = new Parser(path);
            Data = new IniData();
        }

        public void ReadData()
        {
            if (!File.Exists(Parser.IniFile.Path))
                throw new FileNotFound("This file was not found!");
            if (!Parser.IniFile.RightFormat())
                throw new WrongFileFormat("Wrong file format! It should be '.ini'!");
            Parser.IniFile.ReadData();
        }

        public void ParseProperty(string line, string sectionName)
        {
            if (!Data.HasAnySection())
                throw new NoPropertyKeyFound("No Section was found! You need to have a section to add new property!");

            var section = Data.GetSection(sectionName);
            Data.AddProperty(section, Parser.ParsePropertyLine(line));
        }

        public void ParseSection(string line)
        {
            var section = new Section(Parser.ParseSectionName(line));
            if (!Data.HasSection(section))
                Data.AddSection(section);
        }

        public IniData Parse()
        {
            ReadData();
            string currentSection = "";
            foreach (var line in Parser.IniFile.Data)
            {
                if (Parser.LineToIgnore(line))
                    continue;
                if (Parser.IsSection(line))
                {
                    ParseSection(line);
                    currentSection = Parser.ParseSectionName(line);
                }
                else if (Parser.IsProperty(line))
                    ParseProperty(line, currentSection);
            }
            return Data;
        }
    }
}
