using System;
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
            IniParser parser = new IniParser();
            parser.ParseSection("[COMMON]");
            parser.ParseProperty("hello = 1.5", "COMMON");
            var x = parser.Data.Data.Last().Value.Last();
            Console.WriteLine(x.Value);
        }
    }
}
