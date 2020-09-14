using System;
using System.IO;
using IniParser.IniParserException;

namespace IniParser
{
    class IniParser
    {
        private Parser _parser;
        private IniData _data;

        public IniParser()
        {
            _parser = new Parser();
            _data = new IniData();
        }
        public IniParser(string path)
        {
            _parser = new Parser(path);
            _data = new IniData();
        }

        public void ReadData() => _parser.IniFile.ReadData();
        public void ParseProperty(string line, string sectionName)
        {
            if (!_data.AnySectionExists())
                throw new NoPropertyKeyFound("No Section was found! You need to have a section to add new property!");

            var section = _data.GetSection(sectionName);
            if (section == null)
                throw new SectionNotFound($"Section {sectionName} was not found!");
            _data.AddProperty(section, _parser.ParsePropertyLine(line));
        }

        public void ParseSection(string line)
        {
            var section = _parser.ParseSectionLine(line);
            if (!_data.SectionExists(section))
                _data.AddSection(section);
        }

        public IniData Parse()
        {
            ReadData();
            string currentSection = "";
            foreach (var line in _parser.IniFile.Data)
            {
                if (_parser.LineToIgnore(line))
                    continue;
                if (_parser.IsSection(line))
                {
                    ParseSection(line);
                    currentSection = _parser.ParseSectionName(line);
                }
                else if (_parser.IsProperty(line))
                    ParseProperty(line, currentSection);
            }
            return _data;
        }
    }
}
