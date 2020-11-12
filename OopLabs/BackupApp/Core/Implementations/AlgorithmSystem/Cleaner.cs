using System;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class Cleaner : ICleanable
    {
        private readonly BackupSystem.BackupSystem _backupSystem;

        public Cleaner(BackupSystem.BackupSystem backupSystem)
        {
            _backupSystem = backupSystem;
        }

        public void Clean() =>_backupSystem.Backups.ForEach(backup => _backupSystem.Algorithm.Clean(backup));
    }
}
