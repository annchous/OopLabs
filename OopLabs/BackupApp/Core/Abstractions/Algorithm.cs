using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;
using BackupApp.Core.Implementations.RestorePointSystem;

namespace BackupApp.Core.Abstractions
{
    [Serializable]
    public abstract class Algorithm
    {
        public virtual int Calculate(Backup backup)
        {
            var unwantedPointsCount = UnwantedPointsCount(backup);
            var pointsToSave = new List<RestorePoint>();
            pointsToSave.AddRange(backup.RestorePoints.GetRange(unwantedPointsCount, PointsToSaveCount(backup)));

            while (pointsToSave.FirstOrDefault() != null && NeedMorePointsToSave(pointsToSave.FirstOrDefault()) && unwantedPointsCount > 0)
            {
                pointsToSave.Insert(0, backup.RestorePoints[unwantedPointsCount - 1]);
                unwantedPointsCount--;
            }

            return unwantedPointsCount;
        }
        
        public void Clean(ref Backup backup, StorageType storageType)
        {
            var unwantedPointsCount = Calculate(backup);
            for (int i = 0; i < unwantedPointsCount; i++)
            {
                switch (storageType)
                {
                    case StorageType.Separate:
                        backup.RestorePoints[i].Delete();
                        break;
                    case StorageType.Common:
                        if (Directory.Exists(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath)))
                            Directory.Delete(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath), true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(storageType), storageType, null);
                }
            }

            backup.RestorePoints.RemoveRange(0, unwantedPointsCount);
        }

        protected abstract int UnwantedPointsCount(Backup backup);
        protected abstract int PointsToSaveCount(Backup backup);
        protected bool NeedMorePointsToSave(RestorePoint restorePoint)
        {
            if (restorePoint is IncrementalRestorePoint) return false;
            Console.WriteLine("Warning: to implement the algorithm for cleaning restore points, you must store one more point.");
            return true;
        }
    }
}