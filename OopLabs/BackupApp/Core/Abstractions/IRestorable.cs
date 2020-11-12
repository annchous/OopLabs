using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Abstractions
{
    interface IRestorable
    {
        public void CreateRestore(BackupSystem backupSystem);
    }
}
