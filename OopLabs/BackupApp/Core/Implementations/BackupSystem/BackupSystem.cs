using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupApp.CommandLineParser;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem.StorageSystem;
using BackupApp.Core.Implementations.Logger;
using BackupApp.Core.Implementations.RestorePointSystem.RestoreFactory;

namespace BackupApp.Core.Implementations.BackupSystem
{
    [Serializable]
    public class BackupSystem
    {
        public List<Backup> Backups { get; }
        public Algorithm Algorithm { get; }
        public StorageType StorageType { get; }
        public string CommonFolder { get; }

        public BackupSystem(StorageType storageType, string commonFolder, List<string> fileList, Algorithm algorithm)
        {
            StorageType = storageType;
            CommonFolder = commonFolder;
            Backups = new List<Backup>();
            foreach (var file in fileList)
                AddBackup(file, new StorageFolderFactory(storageType, file, commonFolder).GetFolder(), storageType);
            Algorithm = algorithm;
            new BackupLogger().Info($"New backup system with {Backups.Count} backup(s) created.");
        }

        public void CreateRestore(RestoreFactory restoreFactory) => restoreFactory.CreateRestore(this);

        public void RemoveBackup(string filePath)
        {
            var backup = Backups.FirstOrDefault(b => b.OriginalFilePath == filePath);
            if (backup is null)
            {
                new BackupLogger().Error(
                    $"Backup for the file {filePath} does not exist in the current backup system.");
                return;
            }
            Backups.Remove(backup);
            new BackupLogger().Info($"Backup for the file {filePath} was removed from the current backup system.");
        }

        public void AddBackup(string filePath, string backupPath, StorageType storageType)
        {
            if (!File.Exists(filePath))
            {
                new BackupLogger().Error($"Cannot add backup for the file {filePath}." +
                                         $" File {filePath} does not exist.");
                return;
            }
            Backups.Add(new Backup(filePath, backupPath, storageType));
            new BackupLogger().Info($"Backup for the file {filePath} was added to the current backup system.");
        }
    }
}
