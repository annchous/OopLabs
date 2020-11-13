using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BackupApp.CommandLineParser;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.AlgorithmSystem;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.BackupSystem.StorageSystem;
using BackupApp.Core.Implementations.Logger;
using BackupApp.Core.Implementations.RestorePointSystem.RestoreFactory;
using BackupApp.Exception;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace BackupApp.Core.Implementations.ConsoleSystem
{
    public class ConsoleBackupApp : IApp
    {
        private readonly List<string> _args;
        private readonly BinaryFormatter _formatter;
        private string _dataFile;
        private BackupSystem.BackupSystem _backupSystem;

        public ConsoleBackupApp(List<string> args)
        {
            _args = args;
            _formatter = new BinaryFormatter();
        }

        public void Run()
        {
            var parsedData = new Parser(_args).Parse();
            switch (parsedData.ActionType)
            {
                case ActionType.CreateBackupSystem:
                    CreateBackup(parsedData);
                    break;
                case ActionType.CreateRestore:
                    CreateRestorePoint(parsedData);
                    break;
                case ActionType.DeleteBackup:
                    DeleteFile(parsedData);
                    break;
                case ActionType.AddBackup:
                    AddFile(parsedData);
                    break;
                case ActionType.Info:
                    ShowConfigurationInfo(parsedData);
                    break;
                default:
                    throw new ArgumentException(_args[0]);
            }
            SaveData(_dataFile);
        }

        private void CreateBackup(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            if (File.Exists(_dataFile + ".dat"))
            {
                var exception = new DataFileAlreadyExistsException(_dataFile + ".dat");
                BackupLogger.GetInstance().Error(exception.Message);
                throw exception;
            }
            File.Create(_dataFile + ".dat").Close();

            _backupSystem = parsedData.BackupSystem;
            _backupSystem.CreateRestore(new RestoreFactory(RestoreType.Full));
        }

        private void CreateRestorePoint(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            _backupSystem.CreateRestore(new RestoreFactory(parsedData.RestoreType));
            new Cleaner(_backupSystem).Clean();
        }

        private void DeleteFile(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            _backupSystem.Backups.Remove(_backupSystem.Backups
                .FirstOrDefault(x => x.OriginalFilePath == parsedData.FilePath));
        }

        private void AddFile(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            _backupSystem.Backups.Add(new Backup(
                parsedData.FilePath, 
                new StorageFolderFactory(_backupSystem.StorageType, parsedData.FilePath, _backupSystem.CommonFolder).GetFolder(), 
                _backupSystem.StorageType));
        }

        private void ShowConfigurationInfo(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupSystem = ReadData(_dataFile);
            Console.WriteLine();
            Console.WriteLine($"Storage Type: {_backupSystem.StorageType}");
            Console.WriteLine($"Algorithm Type: {_backupSystem.Algorithm.GetType()}");
            Console.WriteLine($"Files for backup amount: {_backupSystem.Backups.Count}");
            Console.WriteLine();
        }

        private void SaveData(string dataFile)
        {
            using var fs = new FileStream(dataFile + ".dat", FileMode.OpenOrCreate);
            _formatter.Serialize(fs, _backupSystem);
        }

        private BackupSystem.BackupSystem ReadData(string dataFile)
        {
            if (!File.Exists(dataFile + ".dat"))
                throw new FileNotFoundException();

            using var fs = new FileStream(dataFile + ".dat", FileMode.OpenOrCreate);
            return (BackupSystem.BackupSystem)_formatter.Deserialize(fs);
        }
    }
}
