using System;
using System.IO;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.BackupSystem.StorageSystem
{
    class SeparateFolderFactory : IFolder
    {
        private readonly string _filePath;
        public SeparateFolderFactory(string filePath)
        {
            _filePath = filePath;
        }

        public string GetFolder() => Directory
            .CreateDirectory(Path.GetDirectoryName(_filePath) + "\\" + Path.GetFileName(_filePath) + "_Backup")
            .ToString();
    }
}
