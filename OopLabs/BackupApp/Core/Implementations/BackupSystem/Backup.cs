using System;
using System.Collections.Generic;
using System.IO;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.ConsoleSystem;
using BackupApp.Core.Implementations.RestorePointSystem;

namespace BackupApp.Core.Implementations.BackupSystem
{
    [Serializable]
    public class Backup
    {
        public Guid Id { get; }
        public string FilePath { get; }
        public string BackupFolderPath { get; }
        public List<RestorePoint> RestorePoints { get; }
        public int RestorePointsCount { get; set; }

        public Backup(string filePath, StorageType storageType)
        {
            Id = Guid.NewGuid();
            FilePath = filePath;
            if (storageType == StorageType.Separate)
            {
                BackupFolderPath = Directory.CreateDirectory
                    (Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileName(FilePath) + "_Backup").ToString();
            }
            RestorePoints = new List<RestorePoint>();
            RestorePointsCount = 0;
        }

        public void CreateRestore(FullRestorePoint restorePoint) => RestorePoints.Add(restorePoint);
        public void CreateRestore(IncrementalRestorePoint restorePoint) => RestorePoints.Add(restorePoint);
    }
}