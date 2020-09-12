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

        public class PropertyKeyNotFound : Exception
        {
            public PropertyKeyNotFound(string message)
                : base(message)
            { }
        }

        public class NoPropertyKeyFound : Exception
        {
            public NoPropertyKeyFound(string message)
                : base(message)
            { }
        }

        public class NotPropertyLine : Exception
        {
            public NotPropertyLine(string message)
                : base(message)
            { }
        }

        public class NotSectionLine : Exception
        {
            public NotSectionLine(string message)
                : base(message)
            { }
        }

        public class WrongParameterValueType : Exception
        {
            public WrongParameterValueType(string message)
                : base(message)
            { }
        }
    }
}
