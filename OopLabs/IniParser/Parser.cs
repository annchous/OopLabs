using System;
using System.Collections.Generic;
using System.Text;
using IniParser.IniParserException;

namespace IniParser
{
    class Parser
    {
        public IniFile IniFile;

        public Parser() {}
        public Parser(string path)
        {
            IniFile = new IniFile(path);
        }

        public string ParseSectionName(string name)
        {
            if (IsSection(name))
                return name.Substring(1, name.Length - 2);
            throw new NotSectionLine("This line is not a section. Section line must starts with '['!");
        }

        public string ParsePropertyKey(string property) => property
            .Substring(0, GetEqualSignIndex(property)).Trim();
        public string ParsePropertyValue(string property) =>
            property.Substring(GetEqualSignIndex(property) + 1, GetPropertyValueLength(property)).Trim();
        public int GetEqualSignIndex(string property) => property.IndexOf('=');
        public int GetCommentStartIndex(string property) => property.IndexOf(';');

        public int GetPropertyValueLength(string property)
        {
            if (HasComment(property))
                return GetCommentStartIndex(property) - GetEqualSignIndex(property) - 1;
            return property.Length - GetEqualSignIndex(property) - 1;
        } 
            

        public Property ParsePropertyLine(string propertyLine)
        {
            if (!IsProperty(propertyLine))
                throw new NotPropertyLine("This line is not a property because it contains no or more than 1 equal (=) sign!");
            if (!ExistsPropertyKey(propertyLine))
                throw new PropertyKeyNotFound("Property key was not found! Invalid property!");
            string key = ParsePropertyKey(propertyLine);
            object value = ParsePropertyValue(propertyLine);
            return new Property(key, value);
        }

        public Section ParseSectionLine(string sectionLine) => new Section(ParseSectionName(sectionLine));
        public bool IsSection(string line) => line.StartsWith('[');
        public bool IsProperty(string line) => line.Contains('=') && line.IndexOf('=') == line.LastIndexOf('=');
        public bool ExistsPropertyKey(string line) => !String.IsNullOrEmpty(ParsePropertyKey(line));
        public bool HasComment(string line) => line.Contains(';');
    }
}
