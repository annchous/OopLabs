using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IniParser.IniParserException;

namespace IniParser
{
    class IniData
    {
        public Dictionary<Section, List<Property>> Data;
        private readonly IFormatProvider _formatter;

        public IniData()
        {
            Data = new Dictionary<Section, List<Property>>();
            _formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
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
        public bool SectionExists(Section section)
        {
            if (GetSection(section.Name) != null)
                if (GetSection(section.Name).GetHashCode() == section.GetHashCode())
                    return true;
            return false;
        }


        public int TryGetInt(string sectionName, string propertyName) => TryGet<int>(sectionName, propertyName);
        public double TryGetDouble(string sectionName, string propertyName) => TryGet<double>(sectionName, propertyName);
        public string TryGetString(string sectionName, string propertyName) => TryGet<string>(sectionName, propertyName);
        public T TryGet<T>(string sectionName, string propertyName)
        {
            var value = GetProperty(sectionName, propertyName);
            if (value == null)
                throw new PropertyKeyNotFound($"Property {propertyName} was not found!");
            var parsedValue = TryParse<T>(value.Value);
            if (parsedValue.GetType() == typeof(T))
                return (T) (parsedValue);
            throw new BadValueCast($"Type of {propertyName} in section {sectionName} can not be converted to {typeof(T).Name}!");
        }
        public object TryParse<T>(string propertyValue)
        {
            if (typeof(T) == typeof(int))
            {
                if (Int32.TryParse(propertyValue, NumberStyles.Integer, _formatter, out int intResult))
                    return intResult;
                throw new BadValueCast($"Type of {propertyValue} can not be converted to {typeof(T).Name}!");
            }
            if (typeof(T) == typeof(double))
            {
                if (Double.TryParse(propertyValue, NumberStyles.Float, _formatter, out double doubleResult))
                    return doubleResult;
                throw new BadValueCast($"Type of {propertyValue} can not be converted to {typeof(T).Name}!");
            }
            return propertyValue;
        }
    }
}
