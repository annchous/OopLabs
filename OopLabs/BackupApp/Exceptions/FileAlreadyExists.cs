using System;

namespace BackupApp.Exceptions
{
    public class FileAlreadyExists : Exception
    {
        public FileAlreadyExists(string fileName) 
            : base($"Cannot create data file! File {fileName} already exists.") {}
    }
}
