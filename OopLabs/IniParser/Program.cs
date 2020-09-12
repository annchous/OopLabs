using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace IniParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //IniParser parser = new IniParser("lab1.ini");
            //parser.ReadData();
            Parser p = new Parser();
            var x = p.ParsePropertyLine("hello = 1.a");
            Console.WriteLine(x.Value.GetType());
        }
    }
}
