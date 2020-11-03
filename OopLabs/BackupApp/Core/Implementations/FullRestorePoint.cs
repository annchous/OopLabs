using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations
{
    [Serializable]
    class FullRestorePoint : RestorePoint
    {
        public string RestorePointFolder { get; }
        public string RestorePointInfoFile { get; }
        public string RestorePointFile { get; }
        public FullRestorePoint(DateTime date, long size, string path, string backupFilePath) 
            : base(date, size, path, backupFilePath)
        {
            RestorePointFolder = Directory.CreateDirectory(path).ToString();
            RestorePointInfoFile = path + "\\RestorePointInfo.txt";
            File.Create(RestorePointInfoFile).Close();
            File.WriteAllLines(RestorePointInfoFile, new []
            {
                Date.ToString(CultureInfo.CurrentCulture),
                Size.ToString()
            });
            RestorePointFile = path + "\\BackupFile.txt";
            File.Copy(BackupFilePath, RestorePointFile);

            Console.WriteLine("Full restore point was created");
        }
    }
}
