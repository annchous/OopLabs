using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Exception
{
    public class DataFileAlreadyExistsException : System.Exception
    {
        public DataFileAlreadyExistsException(string fileName) : base($"Data file {fileName} already exists.") {}
    }
}
