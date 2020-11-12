using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BackupApp.Core.Implementations.Logger;

namespace BackupApp.Core.Abstractions
{
    [Serializable]
    public abstract class RestorePoint
    {
        public DateTime CreationTime { get; }
        public abstract long Size { get; }
        public long Length { get; }
        public string FullPath { get; }
        public string Name { get; }
        public string DirectoryName { get; }
        public string OriginalFilePath { get; }

        protected RestorePoint(string fullPath, string originalFilePath)
        {
            CreationTime = DateTime.Now;
            Length = new FileInfo(originalFilePath).Length;
            FullPath = fullPath;
            Name = new FileInfo(fullPath).Name;
            DirectoryName = new FileInfo(fullPath).DirectoryName;
            OriginalFilePath = originalFilePath;
        }

        public abstract void CreateRestore();

        public void DeleteRestore()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
                new BackupLogger().Info($"Restore point at path {FullPath} was deleted.");
            }
            else new BackupLogger().Error($"Restore point at path {FullPath} does not exist.");
        }
    }
}
