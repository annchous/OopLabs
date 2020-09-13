using System;
using System.Globalization;
using IniParser.IniParserException;

namespace IniParser
{
    class Parser
    {
        public IniFile IniFile;
        private readonly IFormatProvider _formatter;

        public Parser()
        {
            _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
        }
        public Parser(string path)
        {
            IniFile = new IniFile(path);
            _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
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
            object value = TryParse(ParsePropertyValue(propertyLine));
            return new Property(key, value);
        }

        public object TryParse(string propertyValue)
        {
            if (Int32.TryParse(propertyValue, NumberStyles.Integer, _formatter, out int intResult))
                return intResult;
            if (Double.TryParse(propertyValue, NumberStyles.Float, _formatter, out double doubleResult))
                return doubleResult;
            return propertyValue;
        }

        public Section ParseSectionLine(string sectionLine) => new Section(ParseSectionName(sectionLine));
        public bool IsSection(string line) => line.StartsWith('[');
        public bool IsProperty(string line) => line.Contains('=') && line.IndexOf('=') == line.LastIndexOf('=');
        public bool ExistsPropertyKey(string line) => !String.IsNullOrEmpty(ParsePropertyKey(line));
        public bool HasComment(string line) => line.Contains(';');
        public bool CommentOnlyLine(string line) => line.Trim().StartsWith(';');
        public bool EmptyLine(string line) => String.IsNullOrEmpty(line);
        public bool LineToIgnore(string line) =>
            CommentOnlyLine(line) || EmptyLine(line) || (!IsSection(line) && !IsProperty(line));
    }
}
