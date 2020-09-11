using System;

namespace IniParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser("C:\\homework\\abc.ini");
            string toParseString = "key =        value; comment";
            string section = "[COMMON]";
            Console.WriteLine(parser.iniFile.Name);
            Console.WriteLine("input:\n" + section + '\n' + toParseString + '\n' + "output:");
            Console.WriteLine(parser.ParseSectionName(section));
            Console.WriteLine(parser.ParsePropertyKey(toParseString));
            Console.WriteLine(parser.ParsePropertyValue(toParseString));
        }
    }
}
