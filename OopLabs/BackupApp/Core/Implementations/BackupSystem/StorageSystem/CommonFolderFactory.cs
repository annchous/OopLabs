using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.BackupSystem.StorageSystem
{
    class CommonFolderFactory : IFolder
    {
        private readonly string _commonFolder;
        public CommonFolderFactory(string commonFolder)
        {
            _commonFolder = commonFolder;
        }
        public string GetFolder() => _commonFolder;
    }
}
