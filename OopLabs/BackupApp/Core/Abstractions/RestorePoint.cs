using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackupApp.Core.Abstractions
{
    [Serializable]
    abstract class RestorePoint
    {
        public DateTime Date { get; }
        public long Size { get; }
        public long FullSize { get; }
        public string Path { get; }
        public string BackupFilePath { get; }

        public RestorePoint(DateTime date, long size, string path, string backupFilePath)
        {
            Date = date;
            Size = size;
            Path = path;
            BackupFilePath = backupFilePath;
            FullSize = new FileInfo(BackupFilePath).Length;
        }
    }
}
