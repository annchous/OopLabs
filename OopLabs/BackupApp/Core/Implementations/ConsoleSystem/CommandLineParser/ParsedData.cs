using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser
{
    class ParsedData
    {
        public ActionType ActionType { get; }
        public BackupManager BackupManager { get; }
        public string DataFile { get; }
        public BackupType BackupType { get; }
        public string FilePath { get; }

        public ParsedData(ActionType actionType, string dataFile)
        {
            ActionType = actionType;
            DataFile = dataFile;
        }

        public ParsedData(ActionType actionType, string dataFile, BackupType backupType)
        {
            ActionType = actionType;
            DataFile = dataFile;
            BackupType = backupType;
        }

        public ParsedData(ActionType actionType, string dataFile, BackupManager backupManager)
        {
            ActionType = actionType;
            DataFile = dataFile;
            BackupManager = backupManager;
        }

        public ParsedData(ActionType actionType, string dataFile, string filePath)
        {
            ActionType = actionType;
            DataFile = dataFile;
            FilePath = filePath;
        }
    }
}
