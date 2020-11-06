using System;
using System.Globalization;
using System.IO;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.RestorePointSystem
{
    [Serializable]
    public class FullRestorePoint : RestorePoint
    {
        public string RestorePointFolder { get; }
        public string RestorePointInfoFile { get; }
        public string RestorePointFile { get; }
        public FullRestorePoint(DateTime date, long size, string restorePointPath, string backupFilePath) 
            : base(date, size, restorePointPath, backupFilePath)
        {
            RestorePointFolder = Directory.CreateDirectory(restorePointPath).ToString();
            RestorePointInfoFile = restorePointPath + "\\RestorePointInfo.txt";
            File.Create(RestorePointInfoFile).Close();
            File.WriteAllLines(RestorePointInfoFile, new []
            {
                Date.ToString(CultureInfo.CurrentCulture),
                Size.ToString()
            });
            RestorePointFile = restorePointPath + "\\BackupFile.txt";
            File.Copy(BackupFilePath, RestorePointFile);

            Console.WriteLine("Full restore point was created");
        }

        public override void Delete()
        {
            if (Directory.Exists(RestorePointPath))
                Directory.Delete(RestorePointPath, true);
        }
    }
}
