using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.CommandLineParser.Options
{
    class DeleteBackupOption : Option, IParsable
    {
        public DeleteBackupOption(IEnumerable<string> arguments) : base(arguments) {}

        public ParsedData Parse()
        {
            CheckArguments(2);
            var dataFile = ParseDataFile();
            var filePath = ParseFilePath();
            enumerator.Dispose();

            return new ParsedData(ActionType.DeleteBackup, dataFile, filePath);
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
