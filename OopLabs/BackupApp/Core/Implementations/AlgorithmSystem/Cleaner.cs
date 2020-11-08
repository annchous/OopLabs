using System;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class Cleaner : ICleanable
    {
        private readonly BackupManager _backupManager;

        public Cleaner(BackupManager backupManager)
        {
            _backupManager = backupManager;
        }

        public void Clean() =>_backupManager.Backups.ForEach(backup => _backupManager.Algorithm.Clean(backup, _backupManager.StorageType));
    }
}
