using System;
using System.IO;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;
using BackupApp.Core.Implementations.RestorePointSystem;
using BackupApp.Exceptions;

namespace BackupApp.Core.Abstractions
{
    [Serializable]
    public abstract class Algorithm
    {
        public AlgorithmType Type { get; }

        protected Algorithm(AlgorithmType algorithmType)
        {
            Type = algorithmType;
        }

        public abstract int Calculate(Backup backup);

        public void Clean(ref Backup backup, StorageType storageType)
        {
            var unwantedPointsCount = Calculate(backup);
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
                        case StorageType.Unknown:
                            throw new WrongArgumentFormat(storageType.ToString());
                        default:
                            throw new ArgumentOutOfRangeException(nameof(storageType), storageType, null);
                    }

                }
                else
                {
                    switch (storageType)
                    {
                        case StorageType.Separate:
                            if (File.Exists(backup.RestorePoints[i].RestorePointPath))
                                File.Delete(backup.RestorePoints[i].RestorePointPath);
                            break;
                        case StorageType.Common:
                            if (Directory.Exists(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath)))
                                Directory.Delete(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath), true);
                            break;
                        case StorageType.Unknown:
                            throw new WrongArgumentFormat(storageType.ToString());
                        default:
                            throw new ArgumentOutOfRangeException(nameof(storageType), storageType, null);
                    }
                }
            }

            backup.RestorePoints.RemoveRange(0, unwantedPointsCount);
        }
        protected bool Warning(RestorePoint restorePoint)
        {
            if (restorePoint.GetType() != typeof(IncrementalRestorePoint)) return false;
            Console.WriteLine("Warning: to implement the algorithm for cleaning restore points, you must store one more point.");
            return true;
        }
    }
}
