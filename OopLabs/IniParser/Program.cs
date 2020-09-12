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
            IniParser parser = new IniParser("lab1.ini");
            var data = parser.Parse();

            var x = data.TryGet<int>("COMMON", "StatisterTimeMs");
            Console.WriteLine(x);
            
        }
    }
}
