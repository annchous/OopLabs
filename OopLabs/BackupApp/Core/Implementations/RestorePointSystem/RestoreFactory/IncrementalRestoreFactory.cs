using System;
using System.Linq;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.RestorePointSystem.RestoreFactory
{
    class IncrementalRestoreFactory : IRestorable
    {
        public void CreateRestore(BackupSystem.BackupSystem backupSystem)
        {
            foreach (var backup in backupSystem.Backups)
            {
                var incrementalRestorePoint = new IncrementalRestorePoint(
                    $"{backup.FullPath}\\RestorePoint_{backup.RestorePointsCount++}",
                    backup.OriginalFilePath,
                    backup.RestorePoints.LastOrDefault());
                incrementalRestorePoint.CreateRestore();

                backup.CreateRestorePoint(incrementalRestorePoint);
            }
        }
    }
}
