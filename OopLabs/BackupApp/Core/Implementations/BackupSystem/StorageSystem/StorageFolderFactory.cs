using System;
using BackupApp.CommandLineParser;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.BackupSystem.StorageSystem
{
    class StorageFolderFactory : IFolder
    {
        private readonly StorageType _storageType;
        private readonly string _filePath;
        private readonly string _commonFolder;

        public StorageFolderFactory(StorageType storageType, string filePath, string commonFolder)
        {
            _storageType = storageType;
            _filePath = filePath;
            _commonFolder = commonFolder;
        }

        public string GetFolder()
        {
            return _storageType switch
            {
                StorageType.Separate => new SeparateFolderFactory(_filePath).GetFolder(),
                StorageType.Common => new CommonFolderFactory(_commonFolder).GetFolder(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
