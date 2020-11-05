using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Exceptions
{
    public class WrongArgumentFormat : Exception
    {
        public WrongArgumentFormat(string argument) : base($"Wrong argument format! Cannot recognize {argument}.") {}
    }
}
