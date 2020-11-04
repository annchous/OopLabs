using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class Cleaner : ICleanable
    {
        private BackupManager _backupManager;

        public Cleaner(ref BackupManager backupManager)
        {
            _backupManager = backupManager;
        }

        public void Clean()
        {
            switch (_backupManager.AlgorithmType)
            {
                case AlgorithmType.Count:
                    _backupManager.Backups.ForEach(backup => _backupManager.Algorithm.Clean(ref backup, _backupManager.StorageType));
                    break;
                case AlgorithmType.Date:
                    _backupManager.Backups.ForEach(backup => _backupManager.Algorithm.Clean(ref backup, _backupManager.StorageType));
                    break;
                case AlgorithmType.Size:
                    break;
                case AlgorithmType.Hybrid:
                    break;
                case AlgorithmType.Unknown:
                    throw new ArgumentException();
            }
        }
    }
}
