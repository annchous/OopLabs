using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using IniParser.IniParserException;

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

        public Property GetProperty(string sectionName, string propertyName) =>
            Data[GetSection(sectionName)].First(x => x.Name == propertyName);
        public bool HasAnySection() => Data.Keys.Any();
        public bool HasSection(Section section) => Data.ContainsKey(section);

        public int TryGetInt(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName).Value;
            if (value.GetType().Equals(typeof(int)))
                return (int)(value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not int!");
        }

        public double TryGetDouble(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName).Value;
            if (value.GetType().Equals(typeof(double)))
                return (double)(value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not double!");
        }

        public string TryGetString(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName).Value;
            if (value.GetType().Equals(typeof(string)))
                return (string)(value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not string!");
        }

        public T TryGet<T>(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName).Value;
            if (value.GetType().Equals(typeof(T)))
                return (T)(value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not {typeof(T)}!");
        }
    }
}
