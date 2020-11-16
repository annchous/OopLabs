using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Exception
{
    public class DataFileNotFoundException : System.Exception
    {
        public DataFileNotFoundException(string fileName) : base($"Data file {fileName} was not found.") {}
    }
}
