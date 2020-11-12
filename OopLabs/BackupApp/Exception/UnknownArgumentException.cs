using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Exception
{
    class UnknownArgumentException : System.Exception
    {
        public UnknownArgumentException(string argument) : base($"Unknown argument: {argument}.") {}
    }
}
