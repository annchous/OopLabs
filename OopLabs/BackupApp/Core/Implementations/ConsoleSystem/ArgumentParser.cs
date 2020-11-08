using System;
using System.Collections.Generic;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.AlgorithmSystem;
using BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem
{
    public enum AlgorithmType
    {
        Count,
        Date,
        Size,
        Hybrid
    }

    public enum StorageType
    {
        Separate,
        Common
    }

    public enum BackupType
    {
        Full,
        Incremental
    }

    public enum CombinationType
    {
        Max,
        Min
    }

    static class ArgumentParser
    {
        public static ActionType ParseAction(string action) =>
            action.ToLower() switch
            {
                "-n" => ActionType.CreateBackup,
                "-r" => ActionType.CreateRestorePoint,
                "-d" => ActionType.DeleteFile,
                "-a" => ActionType.AddFile,
                "-cfg" => ActionType.ConfigurationInfo,
                _ => throw new WrongArgumentFormat(action)
            };

        public static AlgorithmType ParseAlgorithmType(string algorithm) =>
            algorithm.ToLower() switch
            {
                "-c" => AlgorithmType.Count,
                "-d" => AlgorithmType.Date,
                "-s" => AlgorithmType.Size,
                "-h" => AlgorithmType.Hybrid,
                _ => throw new WrongArgumentFormat(algorithm)
            };

        public static BackupType ParseBackupType(string backupType) =>
            backupType.ToLower() switch
            {
                "full" => BackupType.Full,
                "incremental" => BackupType.Incremental,
                _ => throw new WrongArgumentFormat(backupType)
            };

        public static StorageType ParseStorageType(string storageType) =>
            storageType.ToLower() switch
            {
                "-s" => StorageType.Separate,
                "-c" => StorageType.Common,
                _ => throw new WrongArgumentFormat(storageType)
            };

        public static CombinationType ParseCombinationType(string combinationType) =>
            combinationType.ToLower() switch
            {
                "max" => CombinationType.Max,
                "min" => CombinationType.Min,
                _ => throw new WrongArgumentFormat(combinationType)
            };
    }
}
