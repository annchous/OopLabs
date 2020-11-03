using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BackupApp.Core.Implementations
{
    public enum BackupType
    {
        Full,
        Incremental,
        Unknown
    }

    [Serializable]
    class BackupManager
    {
        public List<Backup> Backups { get; }

        public BackupManager() : this(new List<string>())
        {}

        public BackupManager(List<string> filesList)
        {
            Backups = new List<Backup>();
            foreach (var file in filesList)
            {
                Backups.Add(new Backup(file));
            }
        }

        public void CreateBackup(BackupType backupType)
        {
            switch (backupType)
            {
                case BackupType.Full:
                    Backups.ForEach(backup => backup.CreateRestore
                        (new FullRestorePoint(
                        DateTime.Now, 
                        new FileInfo(backup.FilePath).Length, 
                        backup.BackupFolderPath + "\\RestorePoint_" + backup.RestorePoints.Count, 
                        backup.FilePath))
                    );
                    break;
                case BackupType.Incremental:
                    Backups.ForEach(backup => backup.CreateRestore
                        (new IncrementalRestorePoint
                        (DateTime.Now, 
                        new FileInfo(backup.FilePath).Length - backup.RestorePoints.Last().FullSize, 
                        backup.BackupFolderPath + "\\RestorePoint_" + backup.RestorePoints.Count, 
                        backup.FilePath))
                    );
                    break;
                case BackupType.Unknown:
                    throw new ArgumentException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(backupType), backupType, null);
            }
        }
    }
}
