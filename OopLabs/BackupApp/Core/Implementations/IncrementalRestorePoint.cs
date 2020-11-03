using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations
{
    [Serializable]
    class IncrementalRestorePoint : RestorePoint
    {
        public IncrementalRestorePoint(DateTime date, long size, string path, string backupFilePath)
            : base(date, size, path, backupFilePath)
        {
            File.Create(Path).Close();
            File.WriteAllLines(Path, new[]
            {
                Date.ToString(CultureInfo.CurrentCulture),
                Size.ToString()
            });

            Console.WriteLine("Incremental restore point was created");
        }
    }
}
