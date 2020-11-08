using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser;

namespace BackupApp.Core.Abstractions
{
    interface IParsable
    {
        public ParsedData Parse();
    }
}
