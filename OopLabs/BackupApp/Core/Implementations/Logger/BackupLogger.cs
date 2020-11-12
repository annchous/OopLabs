using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.Logger
{
    class BackupLogger : ILogger
    {
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
