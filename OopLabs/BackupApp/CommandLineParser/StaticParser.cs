using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Exception;

namespace BackupApp.CommandLineParser
{
    public static class StaticParser
    {
        public static ActionType ParseAction(string action) =>
            action.ToLower() switch
            {
                "-n" => ActionType.CreateBackupSystem,
                "-r" => ActionType.CreateRestore,
                "-d" => ActionType.DeleteBackup,
                "-a" => ActionType.AddBackup,
                "-i" => ActionType.Info,
                _ => throw new UnknownArgumentException(action)
            };

        public static AlgorithmType ParseAlgorithmType(string algorithm) =>
            algorithm.ToLower() switch
            {
                "-c" => AlgorithmType.Count,
                "-d" => AlgorithmType.Date,
                "-s" => AlgorithmType.Size,
                "-h" => AlgorithmType.Hybrid,
                _ => throw new UnknownArgumentException(algorithm)
            };

        public static RestoreType ParseBackupType(string backupType) =>
            backupType.ToLower() switch
            {
                "-f" => RestoreType.Full,
                "-i" => RestoreType.Incremental,
                _ => throw new UnknownArgumentException(backupType)
            };

        public static StorageType ParseStorageType(string storageType) =>
            storageType.ToLower() switch
            {
                "-s" => StorageType.Separate,
                "-c" => StorageType.Common,
                _ => throw new UnknownArgumentException(storageType)
            };

        public static CombinationType ParseCombinationType(string combinationType) =>
            combinationType.ToLower() switch
            {
                "max" => CombinationType.Max,
                "min" => CombinationType.Min,
                _ => throw new UnknownArgumentException(combinationType)
            };
    }
}
