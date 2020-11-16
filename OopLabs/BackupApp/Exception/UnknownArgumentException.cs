using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Exception
{
    public class UnknownArgumentException : System.Exception
    {
        public UnknownArgumentException(string argument) : base($"Unknown argument: {argument}.") {}
    }
}
