using System;
using System.Globalization;
using System.IO;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.Logger;

namespace BackupApp.Core.Implementations.RestorePointSystem
{
    [Serializable]
    public class FullRestorePoint : RestorePoint
    {
        public FullRestorePoint(string fullPath, string originalFilePath) 
            : base(fullPath, originalFilePath)
        {
            Size = new FileInfo(originalFilePath).Length;
        }

        public override long Size { get; }
        public override void CreateRestore()
        {
            File.Create(FullPath).Close();
            File.WriteAllLines(FullPath, new []
            {
                "Full restore point\n",
                CreationTime.ToString(CultureInfo.CurrentCulture),
                Size.ToString(),
                Name,
                DirectoryName,
                FullPath,
                OriginalFilePath
            });
            new BackupLogger().Info($"Full restore point at path {FullPath} was created.");
        }
    }
}
