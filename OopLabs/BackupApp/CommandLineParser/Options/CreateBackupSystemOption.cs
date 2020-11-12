using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.AlgorithmSystem;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.BackupSystem.StorageSystem;
using BackupApp.Core.Implementations.Logger;
using BackupApp.Exception;

namespace BackupApp.CommandLineParser.Options
{
    class CreateBackupSystemOption : Option, IParsable
    {
        public CreateBackupSystemOption(IEnumerable<string> arguments) : base(arguments) {}

        public ParsedData Parse()
        {
            CheckArguments(1);
            var storageType = ParseStorageType();
            CheckArguments(storageType == StorageType.Common ? 7 : 6);
            var commonFolder = ParseCommonFolder(storageType);
            var fileList = ParseFilePaths();
            var dataFile = ParseDataFile();
            var algorithmType = ParseAlgorithmType();
            var algorithm = ParseAlgorithm(algorithmType);

            enumerator.Dispose();
            return new ParsedData(ActionType.CreateBackupSystem, dataFile, new BackupSystem(storageType, commonFolder, fileList, algorithm));
        }

        protected override void CheckArguments(int expected)
        {
            if (arguments.Count() >= expected) return;
            var exception = new WrongArgumentAmountException(arguments.Count() + 1, expected + 1);
            new BackupLogger().Error(exception.Message);
            throw exception;
        }

        private StorageType ParseStorageType()
        {
            var result = StaticParser.ParseStorageType(enumerator.Current);
            enumerator.MoveNext();
            return result;
        }

        private string ParseCommonFolder(StorageType storageType)
        {
            var result = storageType == StorageType.Common
                ? enumerator.Current
                : "";
            if (result != "") enumerator.MoveNext();
            if (!Directory.Exists(result) && result != "")
                throw new BackupApp.Exception.FileNotFoundException(result);
            return result;
        }

        private List<string> ParseFilePaths()
        {
            var result = new List<string>();
            while (enumerator.Current != "-df")
            {
                //if (!File.Exists(enumerator.Current))
                //    throw new BackupApp.Exception.FileNotFoundException(enumerator.Current);
                result.Add(enumerator.Current);
                enumerator.MoveNext();
            }
            enumerator.MoveNext();

            return result;
        }

        private AlgorithmType ParseAlgorithmType()
        {
            var result = StaticParser.ParseAlgorithmType(enumerator.Current);
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
            var combinationType = StaticParser.ParseCombinationType(enumerator.Current);
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
