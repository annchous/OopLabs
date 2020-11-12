using System;
using BackupApp.CommandLineParser;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.RestorePointSystem.RestoreFactory
{
    public class RestoreFactory : IRestorable
    {
        private readonly RestoreType _restoreType;

        public RestoreFactory(RestoreType restoreType)
        {
            this._restoreType = restoreType;
        }

        public void CreateRestore(BackupSystem.BackupSystem backupSystem)
        {
            switch (_restoreType)
            {
                case RestoreType.Full:
                    new FullRestoreFactory().CreateRestore(backupSystem);
                    break;
                case RestoreType.Incremental:
                    new IncrementalRestoreFactory().CreateRestore(backupSystem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
