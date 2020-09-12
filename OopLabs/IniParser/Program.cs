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

            foreach (var key in data.Data.Keys)
            {
                Console.WriteLine(key.Name);
                foreach (var value in data.Data[key])
                {
                    Console.WriteLine(value.Name + " " + value.Value + " " + value.Value.GetType());
                }
            }
        }
    }
}
