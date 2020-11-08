using System;
using System.Collections.Generic;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.AlgorithmSystem;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser.Options
{
    class CreateBackupOption : Option, IParsable
    {
        public CreateBackupOption(IEnumerable<string> arguments) : base(arguments) { }

        public ParsedData Parse()
        {
            var storageType = ParseStorageType();
            var commonFolder = ParseCommonFolder(storageType);
            var fileList = ParseFilePaths();
            var dataFile = ParseDataFile();
            var algorithmType = ParseAlgorithmType();
            var algorithm = ParseAlgorithm(algorithmType);

            enumerator.Dispose();
            return new ParsedData(ActionType.CreateBackup, dataFile, new BackupManager(fileList, storageType, algorithmType, algorithm, commonFolder));
        }

        private StorageType ParseStorageType()
        {
            var result = ArgumentParser.ParseStorageType(enumerator.Current);
            enumerator.MoveNext();
            return result;
        }

        private string ParseCommonFolder(StorageType storageType)
        {
            var result = storageType == StorageType.Common
                ? enumerator.Current
                : "";
            if (result != "") enumerator.MoveNext();
            return result;
        }

        private List<string> ParseFilePaths()
        {
            var result = new List<string>();
            while (enumerator.Current != "-df")
            {
                result.Add(enumerator.Current);
                enumerator.MoveNext();
            }
            enumerator.MoveNext();

            return result;
        }

        private AlgorithmType ParseAlgorithmType()
        {
            var result = ArgumentParser.ParseAlgorithmType(enumerator.Current);
            enumerator.MoveNext();
            return result;
        }

        private Algorithm ParseAlgorithm(AlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case AlgorithmType.Count:
                    if (int.TryParse(enumerator.Current, out int count))
                        return new CountAlgorithm(count);
                    else throw new ArgumentException();
                case AlgorithmType.Date:
                    if (DateTime.TryParse(enumerator.Current, out DateTime date))
                        return new DateAlgorithm(date);
                    else throw new ArgumentException();
                case AlgorithmType.Size:
                    if (long.TryParse(enumerator.Current, out long size))
                        return new SizeAlgorithm(size);
                    else throw new ArgumentException();
                case AlgorithmType.Hybrid:
                    enumerator.MoveNext();
                    return ParseHybridAlgorithm();
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, null);
            }
        }

        private HybridAlgorithm ParseHybridAlgorithm()
        {
            var combinationType = ArgumentParser.ParseCombinationType(enumerator.Current);
            var algorithms = new List<Algorithm>();
            while (enumerator.MoveNext())
            {
                var algorithmType = ParseAlgorithmType();
                algorithms.Add(ParseAlgorithm(algorithmType));
            }

            return new HybridAlgorithm(algorithms, combinationType);
        }
    }
}
