using System;
using System.Collections.Generic;
using System.Linq;
using BackupApp.CommandLineParser;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.Logger;

namespace BackupApp.Core.Implementations.BackupSystem
{
    [Serializable]
    public class Backup
    {
        public Guid Id { get; }
        public DateTime CreationTime { get; }
        public string FullPath { get; }
        public string OriginalFilePath { get; }
        public List<RestorePoint> RestorePoints { get; }
        private long Size => RestorePoints.Sum(x => x.Size);
        private StorageType StorageType { get; }
        public int RestorePointsCount { get; set; }

        public Backup(string originalFilePath, string fullPath, StorageType storageType)
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            FullPath = fullPath;
            OriginalFilePath = originalFilePath;
            RestorePoints = new List<RestorePoint>();
            StorageType = storageType;
            RestorePointsCount = 0;
            new BackupLogger().Info($"New backup for the file {OriginalFilePath} at path {FullPath} was created.\n" +
                                    $"Backup ID: {Id}.\nBackup storage type: {StorageType}.");
        }

        public void CreateRestorePoint(RestorePoint restorePoint) => RestorePoints.Add(restorePoint);
        public void DeleteRestorePoint(RestorePoint restorePoint) =>
            RestorePoints.FirstOrDefault(x => x.FullPath == restorePoint.FullPath)?.DeleteRestore();

        public void GetInfo() => Console.WriteLine($"Backup ID: {Id}\n" +
                                                   $"Creation time: {CreationTime}\n" +
                                                   $"Backup size: {Size}\n" +
                                                   $"Restore points amount: {RestorePoints.Count}");
    }
}