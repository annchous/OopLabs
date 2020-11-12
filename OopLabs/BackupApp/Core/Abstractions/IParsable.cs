using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.CommandLineParser;

namespace BackupApp.Core.Abstractions
{
    interface IParsable
    {
        public ParsedData Parse();
    }
}
