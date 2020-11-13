using System;
using System.IO;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.Logger
{
    class BackupLogger : ILogger
    {
        private BackupLogger() {}
        private static BackupLogger _instance;

        public static BackupLogger GetInstance() => _instance ??= new BackupLogger();

        public void Info(string message)
        {
            Log(LogType.Info, message);
        }

        public void Warning(string message)
        {
            Log(LogType.Warning, message);
        }

        public void Error(string message)
        {
            Log(LogType.Error, message);
        }

        public void Log(LogType logType, string message)
        {
            File.AppendAllLines("BackupApp.log", new[] {$"[{logType}]: {message}\n"});
        }
    }
}
