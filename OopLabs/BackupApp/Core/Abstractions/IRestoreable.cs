using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Abstractions
{
    interface IRestoreable
    {
        public void CreateRestore(BackupSystem backupSystem);
    }
}
