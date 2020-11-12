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
        public string GetFolder()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) 
                       + "\\" + Path.GetFileName(_filePath) + "_Backup";
            return Directory.CreateDirectory(path).ToString();
        }
    }
}
