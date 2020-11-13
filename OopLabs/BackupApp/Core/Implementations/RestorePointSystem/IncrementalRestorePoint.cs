using System;
using System.Globalization;
using System.IO;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.Logger;

namespace BackupApp.Core.Implementations.RestorePointSystem
{
    [Serializable]
    class IncrementalRestorePoint : RestorePoint
    {
        public IncrementalRestorePoint(string fullPath, string originalFilePath, RestorePoint previousRestorePoint) 
            : base(fullPath, originalFilePath)
        {
            Size = new FileInfo(originalFilePath).Length - previousRestorePoint.Length;
        }

        public override long Size { get; }
        public override void CreateRestore()
        {
            File.Create(FullPath).Close();
            File.WriteAllLines(FullPath, new[]
            {
                "Incremental restore point\n",
                CreationTime.ToString(CultureInfo.CurrentCulture),
                Size.ToString(),
                Name,
                DirectoryName,
                FullPath,
                OriginalFilePath
            });
            BackupLogger.GetInstance().Info($"Incremental restore point at path {FullPath} was created.");
        }
    }
}
