using System;
using System.Collections.Generic;
using System.Linq;
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

        public Section GetSection(string sectionName) => Data.FirstOrDefault(x => x.Key.Name == sectionName).Key;

        public Property GetProperty(string sectionName, string propertyName)
        {
            if (GetSection(sectionName) == null)
                throw new SectionNotFound($"Section {sectionName} was not found!");
            return Data[GetSection(sectionName)].FirstOrDefault(x => x.Name == propertyName);
        }
        public bool AnySectionExists() => Data.Keys.Any();
        public bool SectionExists(Section section) => Data.ContainsKey(section);

        public int TryGetInt(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName);
            if (value == null)
                throw new PropertyKeyNotFound($"Property {propertyName} was not found!");
            if (value.Value.GetType() == typeof(int))
                return (int)(value.Value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not {typeof(int).Name}!");
        }

        public double TryGetDouble(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName);
            if (value == null)
                throw new PropertyKeyNotFound($"Property {propertyName} was not found!");
            if (value.Value.GetType() == typeof(double))
                return (double)(value.Value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not {typeof(double).Name}!");
        }

        public string TryGetString(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName);
            if (value == null)
                throw new PropertyKeyNotFound($"Property {propertyName} was not found!");
            if (value.Value.GetType() == typeof(string))
                return (string)(value.Value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not {typeof(string).Name}!");
        }

        public T TryGet<T>(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName);
            if (value == null)
                throw new PropertyKeyNotFound($"Property {propertyName} was not found!");
            if (value.Value.GetType() == typeof(T))
                return (T)(value.Value);
            throw new WrongParameterValueType($"Type of {propertyName} in section {sectionName} is not {typeof(T).Name}!");
        }
    }
}
