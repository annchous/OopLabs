using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IniParserTest")]

namespace IniParser
{
    class Program
    {
        static void Main(string[] args)
        {
            IniParser parser = new IniParser("lab1.data.ini");
            var data = parser.Parse();

            var x = data.TryGet<int>("COMMON", "hello");
            Console.WriteLine(x);
            
        }
    }
}
