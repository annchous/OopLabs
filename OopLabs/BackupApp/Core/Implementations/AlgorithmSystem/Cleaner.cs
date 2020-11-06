using System;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class Cleaner : ICleanable
    {
        private readonly BackupManager _backupManager;

        public Cleaner(ref BackupManager backupManager)
        {
            _backupManager = backupManager;
        }

        public void Clean() =>_backupManager.Backups.ForEach(backup => _backupManager.Algorithm.Clean(ref backup, _backupManager.StorageType));
    }
}
