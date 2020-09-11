using System;
using System.Collections.Generic;
using System.Text;

namespace IniParser
{
    class Parser
    {
        public IniFile iniFile;
        public Dictionary<Section, Property> Values;

        public Parser(string path)
        {
            iniFile = new IniFile(path);
            Values = new Dictionary<Section, Property>();
        }

        public string ParseSectionName(string name) => name.Substring(1, name.Length - 2);
        public string ParsePropertyKey(string property) => property
            .Substring(0, GetEqualSignIndex(property)).Trim();
        public string ParsePropertyValue(string property) =>
            property.Substring(GetEqualSignIndex(property) + 1, GetPropertyValueLength(property)).Trim();
        public int GetEqualSignIndex(string property) => property.IndexOf('=');
        public int GetCommentStartIndex(string property) => property.IndexOf(';');
        public int GetPropertyValueLength(string property) => 
            GetCommentStartIndex(property) - GetEqualSignIndex(property) - 1;
    }
}
