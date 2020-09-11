using System;
using System.Collections.Generic;
using System.Text;

namespace IniParser
{
    class Handler
    {
        public Parser parser;

        public Handler(string path)
        {
            parser = new Parser(path);
        }
    }
}
