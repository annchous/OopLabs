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

            var x = data.TryGet<double>("COMMON", "hello");
            Console.WriteLine(x + " " + x.GetType().Name);

            foreach (var key in data.Data.Keys)
            {
                Console.WriteLine(key.Name);
            }
        }
    }
}
