using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.CommandLineParser.Options
{
    class CreateRestoreOption : Option, IParsable
    {
        public CreateRestoreOption(IEnumerable<string> arguments) : base(arguments) {}

        public ParsedData Parse()
        {
            CheckArguments(2);
            var dataFile = ParseDataFile();
            var backupType = StaticParser.ParseBackupType(enumerator.Current);
            enumerator.Dispose();

            return new ParsedData(ActionType.CreateRestore, dataFile, backupType);
        }
    }
}
