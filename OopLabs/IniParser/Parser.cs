using System;
using System.Globalization;
using IniParser.IniParserException;

namespace IniParser
{
    class Parser
    {
        public IniFile IniFile;
        

        public Parser()
        { }
        public Parser(string path)
        {
            IniFile = new IniFile(path);
        }

        public string ParseSectionName(string sectionName)
        {
            if (IsSection(sectionName))
                return sectionName.Replace("[", "").Replace("]", "").Trim();
            throw new NotSectionLine("This line is not a section. Section line must starts with '['!");
        }

        public string ParsePropertyKey(string property) => property
            .Substring(0, GetEqualSignIndex(property)).Trim();
        public string ParsePropertyValue(string property) =>
            property.Substring(GetEqualSignIndex(property) + 1, 
                property.Length - GetEqualSignIndex(property) - 1).Trim();
        public int GetEqualSignIndex(string property) => property.IndexOf('=');


        public Property ParsePropertyLine(string propertyLine)
        {
            if (!IsProperty(propertyLine))
                throw new NotPropertyLine("This line is not a property because it contains no or more than 1 equal (=) sign!");
            if (!ExistsPropertyKey(propertyLine))
                throw new PropertyKeyNotFound("Property key was not found! Invalid property!");
            string key = ParsePropertyKey(propertyLine);
            string value = ParsePropertyValue(propertyLine);
            return new Property(key, value);
        }

        public Section ParseSectionLine(string sectionLine) => new Section(ParseSectionName(sectionLine));
        public bool IsSection(string line) => line.StartsWith('[');
        public bool IsProperty(string line) => line.Contains('=') && line.IndexOf('=') == line.LastIndexOf('=');
        public bool ExistsPropertyKey(string line) => !String.IsNullOrEmpty(ParsePropertyKey(line));
        public bool HasComment(string line) => line.Contains(';');
        public bool CommentOnlyLine(string line) => line.Trim().StartsWith(';');
        public bool EmptyLine(string line) => String.IsNullOrEmpty(line);
        public bool LineToIgnore(string line) =>
            EmptyLine(line) || (!IsSection(line) && !IsProperty(line));
    }
}
