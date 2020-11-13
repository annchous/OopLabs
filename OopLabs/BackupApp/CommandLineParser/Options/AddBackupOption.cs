using System;
using System.Collections.Generic;
using System.IO;
using BackupApp.Core.Abstractions;

namespace BackupApp.CommandLineParser.Options
{
    class AddBackupOption : Option, IParseable
    {
        public AddBackupOption(IEnumerable<string> arguments) : base(arguments) {}

        public ParsedData Parse()
        {
            CheckArguments(2);
            var dataFile = ParseDataFile();
            var filePath = ParseFilePath();
            enumerator.Dispose();

            return new ParsedData(ActionType.AddBackup, dataFile, filePath);
        }

        private string ParseFilePath()
        {
            var result = enumerator.Current;
            enumerator.MoveNext();
            if (!File.Exists(result))
                throw new BackupApp.Exception.FileNotFoundException(result);
            return result;
        }
    }
}
