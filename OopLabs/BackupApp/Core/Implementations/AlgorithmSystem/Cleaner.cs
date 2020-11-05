using System;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class Cleaner : ICleanable
    {
        private readonly BackupManager _backupManager;

        public Cleaner(ref BackupManager backupManager)
        {
            _backupManager = backupManager;
        }

        public void Clean()
        {
            switch (_backupManager.AlgorithmType)
            {
                case AlgorithmType.Count:
                case AlgorithmType.Date:
                case AlgorithmType.Size:
                case AlgorithmType.Hybrid:
                    _backupManager.Backups.ForEach(backup => _backupManager.Algorithm.Clean(ref backup, _backupManager.StorageType));
                    break;
                case AlgorithmType.Unknown:
                    throw new ArgumentException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
