using System;
using System.Collections.Generic;
using System.Text;

namespace IniParser
{
    class Handler
    {
        public Parser parser;
        public Dictionary<Section, Property> Values;

        public Handler(string path)
        {
            parser = new Parser(path);
            Values = new Dictionary<Section, Property>();
        }
    }
}
