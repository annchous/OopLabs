using System;
using System.Collections.Generic;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.AlgorithmSystem;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem
{
    public enum ActionType
    {
        CreateBackup,
        OpenBackup,
        DeleteBackup,
        AddFile,
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

    public enum StorageType
    {
        Separate,
        Common,
        Unknown
    }

    public enum BackupType
    {
        Full,
        Incremental,
        Unknown
    }

    public enum CombinationType
    {
        Max,
        Min,
        Unknown
    }

    static class ArgumentParser
    {
        public static ActionType ParseAction(string action) =>
            action.ToLower() switch
            {
                "n" => ActionType.CreateBackup,
                "e" => ActionType.OpenBackup,
                "d" => ActionType.DeleteBackup,
                "a" => ActionType.AddFile,
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

        public static StorageType ParseStorageType(string storageType) =>
            storageType.ToLower() switch
            {
                "sep" => StorageType.Separate,
                "com" => StorageType.Common,
                _ => StorageType.Unknown
            };

        private static CombinationType ParseCombinationType(string combinationType) =>
            combinationType.ToLower() switch
            {
                "max" => CombinationType.Max,
                "min" => CombinationType.Min,
                _ => CombinationType.Unknown
            };

        public static Algorithm ParseAlgorithm(AlgorithmType algorithmType, string[] args, int index)
        {
            switch (algorithmType)
            {
                case AlgorithmType.Count:
                    if (int.TryParse(args[index], out int count))
                        return new CountAlgorithm(count);
                    else throw new ArgumentException();
                case AlgorithmType.Date:
                    if (DateTime.TryParse(args[index], out DateTime date))
                        return new DateAlgorithm(date);
                    else throw new ArgumentException();
                case AlgorithmType.Size:
                    if (long.TryParse(args[index], out long size))
                        return new SizeAlgorithm(size);
                    else throw new ArgumentException();
                case AlgorithmType.Hybrid:
                    return ParseHybridAlgorithm(args, ++index);
                case AlgorithmType.Unknown:
                    throw new WrongArgumentFormat(args[index]);
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, null);
            }
        }

        private static HybridAlgorithm ParseHybridAlgorithm(string[] args, int index)
        {
            var combinationType = ParseCombinationType(args[index++]);
            var algorithms = new List<Algorithm>();
            for (int i = index; i < args.Length; i += 2)
            {
                var algorithmType = ParseAlgorithmType(args[i]);
                algorithms.Add(ParseAlgorithm(algorithmType, args, i + 1));
            }

            return new HybridAlgorithm(algorithms, combinationType);
        }
    }
}
