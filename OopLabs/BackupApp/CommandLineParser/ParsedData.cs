using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.CommandLineParser
{
    class ParsedData
    {
        public ActionType ActionType { get; }
        public BackupSystem BackupSystem { get; }
        public RestoreType RestoreType { get; }
        public string DataFile { get; }
        public string FilePath { get; }

        public ParsedData(ActionType actionType, string dataFile)
        {
            ActionType = actionType;
            DataFile = dataFile;
        }

        public ParsedData(ActionType actionType, string dataFile, RestoreType restoreType)
        {
            ActionType = actionType;
            DataFile = dataFile;
            RestoreType = restoreType;
        }

        public ParsedData(ActionType actionType, string dataFile, BackupSystem backupSystem)
        {
            ActionType = actionType;
            DataFile = dataFile;
            BackupSystem = backupSystem;
        }

        public ParsedData(ActionType actionType, string dataFile, string filePath)
        {
            ActionType = actionType;
            DataFile = dataFile;
            FilePath = filePath;
        }
    }
}
