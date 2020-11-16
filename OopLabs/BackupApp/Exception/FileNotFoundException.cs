using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Exception
{
    public class FileNotFoundException : System.Exception
    {
        public FileNotFoundException(string filePath) : base($"File {filePath} was not found.") {}
    }
}
