using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;
using BackupApp.Core.Implementations.RestorePointSystem;

namespace BackupApp.Core.Abstractions
{
    [Serializable]
    public abstract class Algorithm
    {
        public AlgorithmType Type { get; }

        public Algorithm(AlgorithmType algorithmType)
        {
            Type = algorithmType;
        }

        public abstract int Calculate(ref Backup backup);

        public void Clean(ref Backup backup, StorageType storageType)
        {
            var unwantedPointsCount = Calculate(ref backup);
            for (int i = 0; i < unwantedPointsCount; i++)
            {
                if (backup.RestorePoints[i].GetType() == typeof(FullRestorePoint))
                {
                    switch (storageType)
                    {
                        case StorageType.Separate:
                            if (Directory.Exists(backup.RestorePoints[i].RestorePointPath))
                                Directory.Delete(backup.RestorePoints[i].RestorePointPath, true);
                            break;
                        case StorageType.Common:
                            if (Directory.Exists(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath)))
                                Directory.Delete(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath), true);
                            break;
                    }

                }
                else if (File.Exists(backup.RestorePoints[i].RestorePointPath))
                    File.Delete(backup.RestorePoints[i].RestorePointPath);
            }

            backup.RestorePoints.RemoveRange(0, unwantedPointsCount);
        }
    }
}
