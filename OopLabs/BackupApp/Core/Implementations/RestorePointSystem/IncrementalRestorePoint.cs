using System;
using System.Globalization;
using System.IO;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.RestorePointSystem
{
    [Serializable]
    public class IncrementalRestorePoint : RestorePoint
    {
        public IncrementalRestorePoint(DateTime date, long size, string restorePointPath, string backupFilePath)
            : base(date, size, restorePointPath, backupFilePath)
        {
            File.Create(RestorePointPath).Close();
            File.WriteAllLines(RestorePointPath, new[]
            {
                Date.ToString(CultureInfo.CurrentCulture),
                Size.ToString()
            });

            Console.WriteLine("Incremental restore point was created");
        }
    }
}
