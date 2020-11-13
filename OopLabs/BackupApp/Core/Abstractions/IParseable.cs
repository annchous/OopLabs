using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.CommandLineParser;

namespace BackupApp.Core.Abstractions
{
    public interface IParseable
    {
        public ParsedData Parse();
    }
}
