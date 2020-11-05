using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.ConsoleSystem;
using BackupApp.Core.Implementations.RestorePointSystem;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.BackupSystem
{
    [Serializable]
    public class BackupManager
    {
        public List<Backup> Backups { get; }
        public StorageType StorageType { get; }
        public AlgorithmType AlgorithmType { get; }
        public string CommonFolder { get; }
        public Algorithm Algorithm { get; set; }

        public BackupManager(List<string> filesList, StorageType storageType, AlgorithmType algorithmType, string commonFolder)
        {
            Backups = new List<Backup>();
            StorageType = storageType;
            AlgorithmType = algorithmType;
            CommonFolder = !string.IsNullOrEmpty(commonFolder)
                ? Directory.CreateDirectory(commonFolder).ToString()
                : commonFolder;
            foreach (var file in filesList)
            {
                Backups.Add(new Backup(file, storageType));
            }
        }

        public void CreateBackup(BackupType backupType)
        {
            switch (backupType)
            {
                case BackupType.Full:
                    CreateFullBackup();
                    break;
                case BackupType.Incremental:
                    CreateIncrementalBackup();
                    break;
                case BackupType.Unknown:
                    throw new WrongArgumentFormat(backupType.ToString());
                default:
                    throw new ArgumentOutOfRangeException(nameof(backupType), backupType, null);
            }
        }

        private void CreateFullBackup()
        {
            switch (StorageType)
            {
                case StorageType.Separate:
                    Backups.ForEach(backup => backup.CreateRestore
                        (new FullRestorePoint(
                            DateTime.Now,
                            new FileInfo(backup.FilePath).Length,
                            backup.BackupFolderPath + "\\RestorePoint_" + backup.RestorePointsCount++,
                            backup.FilePath))
                    );
                    break;
                case StorageType.Common:
                    Backups.ForEach(backup => backup.CreateRestore
                        (new FullRestorePoint(
                            DateTime.Now,
                            new FileInfo(backup.FilePath).Length,
                            CommonFolder + "\\RestorePoint_" + backup.RestorePointsCount++ + "\\Backup_" + Path.GetFileName(backup.FilePath),
                            backup.FilePath))
                    );
                    break;
                case StorageType.Unknown:
                    throw new WrongArgumentFormat(StorageType.ToString());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateIncrementalBackup()
        {
            switch (StorageType)
            {
                case StorageType.Separate:
                    Backups.ForEach(backup => backup.CreateRestore
                        (new IncrementalRestorePoint
                        (DateTime.Now,
                            new FileInfo(backup.FilePath).Length - backup.RestorePoints.Last().FullSize,
                            backup.BackupFolderPath + "\\RestorePoint_" + backup.RestorePointsCount++,
                            backup.FilePath))
                    );
                    break;
                case StorageType.Common:
                    Backups.ForEach(backup => backup.CreateRestore
                        (new IncrementalRestorePoint
                        (DateTime.Now,
                            new FileInfo(backup.FilePath).Length - backup.RestorePoints.Last().FullSize,
                            CommonFolder + "\\RestorePoint_" + backup.RestorePointsCount++ + "\\Backup_" + Path.GetFileName(backup.FilePath),
                            backup.FilePath))
                    );
                    break;
                case StorageType.Unknown:
                    throw new WrongArgumentFormat(StorageType.ToString());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
