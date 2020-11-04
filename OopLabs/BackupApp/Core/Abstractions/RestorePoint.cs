using System;
using System.IO;

namespace BackupApp.Core.Abstractions
{
    [Serializable]
    public abstract class RestorePoint
    {
        public DateTime Date { get; }
        public long Size { get; }
        public long FullSize { get; }
        public string RestorePointPath { get; }
        public string BackupFilePath { get; }

        public RestorePoint(DateTime date, long size, string restorePointPath, string backupFilePath)
        {
            Date = date;
            Size = size;
            RestorePointPath = restorePointPath;
            BackupFilePath = backupFilePath;
            FullSize = new FileInfo(BackupFilePath).Length;
        }
    }
}
