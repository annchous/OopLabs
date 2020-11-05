using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.AlgorithmSystem;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem
{
    class ConsoleBackupApp : IApp
    {
        private readonly string[] _args;
        private readonly BinaryFormatter _formatter;
        private string _dataFile;
        public ConsoleBackupApp(string[] args)
        {
            _args = args;
            _formatter = new BinaryFormatter();
        }

        public void Run()
        {
            switch (ArgumentParser.ParseAction(_args[0]))
            {
                case ActionType.CreateBackup:
                    CreateBackup(); 
                    break;
                case ActionType.OpenBackup:
                    CreateRestorePoint();
                    break;
                case ActionType.DeleteBackup:
                    DeleteBackup();
                    break;
                case ActionType.AddFile:
                    AddFile();
                    break;
                case ActionType.Unknown:
                    throw new WrongArgumentFormat(_args[0]);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateBackup()
        {
            var i = 1;
            var storageType = ArgumentParser.ParseStorageType(_args[i++]);
            var commonFolder = storageType == StorageType.Common ? _args[i++] : "";
            var fileList = new List<string>();

            for (; ; i++)
            {
                if (_args[i] == "|") break;
                if (!File.Exists(_args[i]))
                    throw new FileNotFoundException();

                fileList.Add(_args[i]);
            }

            if (File.Exists(_args[++i]))
                throw new FileAlreadyExists(_args[i]);
            _dataFile = _args[i++];

            try
            {
                File.Create(_dataFile + ".dat").Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            var algorithmType = ArgumentParser.ParseAlgorithmType(_args[i++]);
            var backupManager = new BackupManager(fileList, storageType, algorithmType, commonFolder)
            {
                Algorithm = ArgumentParser.ParseAlgorithm(algorithmType, _args, i)
            };

            backupManager.CreateBackup(BackupType.Full);
            new Cleaner(ref backupManager).Clean();
            SaveData(_dataFile, ref backupManager);
        }

        private void CreateRestorePoint()
        {
            if (_args.Length != 3)
                throw new WrongArgumentFormat(string.Join(" ", _args));

            _dataFile = _args[1];
            BackupManager backupManager = ReadData(_dataFile);
            backupManager.CreateBackup(ArgumentParser.ParseBackupType(_args[2]));

            new Cleaner(ref backupManager).Clean();
            SaveData(_dataFile, ref backupManager);
        }

        private void DeleteBackup()
        {
            if (_args.Length != 3)
                throw new WrongArgumentFormat(string.Join(" ", _args));

            _dataFile = _args[1];
            BackupManager backupManager = ReadData(_dataFile);

            backupManager.Backups.Remove(backupManager.Backups.FirstOrDefault(x => x.FilePath == _args[2]));
            backupManager.CreateBackup(BackupType.Full);

            new Cleaner(ref backupManager).Clean();
            SaveData(_dataFile, ref backupManager);
        }

        private void AddFile()
        {
            if (_args.Length != 3)
                throw new WrongArgumentFormat(string.Join(" ", _args));

            _dataFile = _args[1];
            BackupManager backupManager = ReadData(_dataFile);

            if (!File.Exists(_args[2]))
                throw new FileNotFoundException();

            backupManager.Backups.Add(new Backup(_args[2], backupManager.StorageType));
            backupManager.CreateBackup(BackupType.Full);

            new Cleaner(ref backupManager).Clean();
            SaveData(_dataFile, ref backupManager);
        }

        private void SaveData(string dataFile, ref BackupManager backupManager)
        {
            using var fs = new FileStream(dataFile + ".dat", FileMode.OpenOrCreate);
            _formatter.Serialize(fs, backupManager);
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
