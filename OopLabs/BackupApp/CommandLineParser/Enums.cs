using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.CommandLineParser
{
    public enum ActionType
    {
        CreateBackupSystem,
        CreateRestore,
        DeleteBackup,
        AddBackup,
        Info
    }
    public enum StorageType
    {
        Separate,
        Common
    }

    public enum RestoreType
    {
        Full,
        Incremental
    }

    public enum AlgorithmType
    {
        Count,
        Date,
        Size,
        Hybrid
    }
    public enum CombinationType
    {
        Max,
        Min
    }
}
