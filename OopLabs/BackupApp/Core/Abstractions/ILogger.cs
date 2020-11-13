using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Core.Abstractions
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    interface ILogger
    {
        public void Info(string message);
        public void Warning(string message);
        public void Error(string message);
        public void Log(LogType logType, string message);
    }
}
