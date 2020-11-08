using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.AlgorithmSystem;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem
{
    class ConsoleBackupApp : IApp
    {
        private readonly List<string> _args;
        private readonly BinaryFormatter _formatter;
        private string _dataFile;
        private BackupManager _backupManager;

        public ConsoleBackupApp(List<string> args)
        {
            _args = args;
            _formatter = new BinaryFormatter();
        }

        public void Run()
        {
            var parsedData = new CommandLineParser.CommandLineParser(_args).Parse();
            switch (parsedData.ActionType)
            {
                case ActionType.CreateBackup:
                    CreateBackup(parsedData); 
                    break;
                case ActionType.CreateRestorePoint:
                    CreateRestorePoint(parsedData);
                    break;
                case ActionType.DeleteFile:
                    DeleteFile(parsedData);
                    break;
                case ActionType.AddFile:
                    AddFile(parsedData);
                    break;
                case ActionType.ConfigurationInfo:
                    ShowConfigurationInfo(parsedData);
                    break;
                default:
                    throw new WrongArgumentFormat(_args[0]);
            }
            SaveData(_dataFile);
        }

        private void CreateBackup(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            if (File.Exists(_dataFile + ".dat"))
                throw new FileAlreadyExists(_dataFile + ".dat");
            File.Create(_dataFile + ".dat").Close();

            _backupManager = parsedData.BackupManager;
            _backupManager.CreateBackup(BackupType.Full);
            new Cleaner(_backupManager).Clean();
        }

        private void CreateRestorePoint(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupManager = ReadData(_dataFile);
            _backupManager.CreateBackup(parsedData.BackupType);
            new Cleaner(_backupManager).Clean();
        }

        private void DeleteFile(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupManager = ReadData(_dataFile);
            _backupManager.Backups.Remove(_backupManager.Backups.FirstOrDefault(x => x.FilePath == parsedData.FilePath));
        }

        private void AddFile(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupManager = ReadData(_dataFile);
            _backupManager.Backups.Add(new Backup(parsedData.FilePath, _backupManager.StorageType));
        }

        private void ShowConfigurationInfo(ParsedData parsedData)
        {
            _dataFile = parsedData.DataFile;
            _backupManager = ReadData(_dataFile);
            Console.WriteLine();
            Console.WriteLine($"Storage Type: {_backupManager.StorageType}");
            Console.WriteLine($"Algorithm Type: {_backupManager.AlgorithmType}");
            Console.WriteLine($"Files for backup amount: {_backupManager.Backups.Count}");
            Console.WriteLine();
        }

        private void SaveData(string dataFile)
        {
            using var fs = new FileStream(dataFile + ".dat", FileMode.OpenOrCreate);
            _formatter.Serialize(fs, _backupManager);
        }

        private BackupManager ReadData(string dataFile)
        {
            if (!File.Exists(dataFile + ".dat"))
                throw new FileNotFoundException();

            using var fs = new FileStream(dataFile + ".dat", FileMode.OpenOrCreate);
            return (BackupManager)_formatter.Deserialize(fs);
        }
    }
}
