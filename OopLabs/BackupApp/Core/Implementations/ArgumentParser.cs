using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Core.Implementations
{
    public enum ActionType
    {
        CreateBackup,
        OpenBackup,
        DeleteBackup,
        Unknown
    }

    public enum AlgorithmType
    {
        Count,
        Date,
        Size,
        Hybrid,
        Unknown
    }

    static class ArgumentParser
    {
        public static ActionType ParseAction(string action) =>
            action switch
            {
                "n" => ActionType.CreateBackup,
                "e" => ActionType.OpenBackup,
                "d" => ActionType.DeleteBackup,
                _ => ActionType.Unknown
            };

        public static AlgorithmType ParseAlgorithmType(string algorithm) =>
            algorithm.ToLower() switch
            {
                "count" => AlgorithmType.Count,
                "date" => AlgorithmType.Date,
                "size" => AlgorithmType.Size,
                "hybrid" => AlgorithmType.Hybrid,
                _ => AlgorithmType.Unknown
            };

        public static BackupType ParseBackupType(string backupType) =>
            backupType.ToLower() switch
            {
                "full" => BackupType.Full,
                "incremental" => BackupType.Incremental,
                _ => BackupType.Unknown
            };
    }
}
