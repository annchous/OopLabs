using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations
{
    [Serializable]
    class Backup
    {
        public Guid Id { get; }
        public string FilePath { get; }
        public string BackupFolderPath { get; }
        public List<RestorePoint> RestorePoints { get; }

        public Backup(string filePath)
        {
            Id = Guid.NewGuid();
            FilePath = filePath;
            BackupFolderPath = Directory.CreateDirectory
                (Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileName(FilePath) + "_Backup").ToString();
            RestorePoints = new List<RestorePoint>();
        }

        public void CreateRestore(FullRestorePoint restorePoint) => RestorePoints.Add(restorePoint);
        public void CreateRestore(IncrementalRestorePoint restorePoint) => RestorePoints.Add(restorePoint);
    }
}