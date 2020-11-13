using System;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.RestorePointSystem.RestoreFactory
{
    class FullRestoreFactory : IRestoreable
    {
        public void CreateRestore(BackupSystem.BackupSystem backupSystem)
        {
            foreach (var backup in backupSystem.Backups)
            {
                var fullRestorePoint = new FullRestorePoint(
                    $"{backup.FullPath}\\RestorePoint_{backup.RestorePointsCount++}", 
                    backup.OriginalFilePath);
                fullRestorePoint.CreateRestore();

                backup.CreateRestorePoint(fullRestorePoint);
            }
        }
    }
}
