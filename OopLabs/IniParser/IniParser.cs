using System;
using System.IO;
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
            if (!Data.AnySectionExists())
                throw new NoPropertyKeyFound("No Section was found! You need to have a section to add new property!");

            var section = Data.GetSection(sectionName);
            if (section == null)
                throw new SectionNotFound($"Section {sectionName} was not found!");
            Data.AddProperty(section, Parser.ParsePropertyLine(line));
        }

        public void ParseSection(string line)
        {
            var section = Parser.ParseSectionLine(line);
            if (!Data.SectionExists(section))
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
