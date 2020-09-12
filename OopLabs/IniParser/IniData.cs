using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IniParser
{
    class IniData
    {
        public Dictionary<Section, List<Property>> Data;

        public IniData()
        {
            Data = new Dictionary<Section, List<Property>>();
        }

        public void AddSection(Section section) => Data.Add(section, new List<Property>());
        public void AddProperty(Section section, Property property) => Data[section].Add(property);
        public Section GetSection(string sectionName) => Data.First(x => x.Key.Name == sectionName).Key;
        public bool HasAnySection() => Data.Keys.Any();
        public bool HasSection(Section section) => Data.ContainsKey(section);
    }
}
