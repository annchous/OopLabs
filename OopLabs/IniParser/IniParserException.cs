using System;
using System.Collections.Generic;
using System.Text;

namespace IniParser
{
    namespace IniParserException
    {
        public class FileNotFound : Exception
        { 
            public FileNotFound(string message)
                : base(message)
            { }
        }

        public class WrongFileFormat : Exception
        {
            public WrongFileFormat(string message)
                : base(message)
            { }
        }

    }
}
