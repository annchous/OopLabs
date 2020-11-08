using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BackupApp.Core.Abstractions;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser
{
    class AddFileOption : Option, IParsable
    {
        public AddFileOption(IEnumerable<string> arguments) : base(arguments) {}
        public ParsedData Parse()
        {
            if (arguments.Count() != 2)
                throw new WrongArgumentFormat(string.Join(" ", arguments.ToString()));

            var dataFile = ParseDataFile();
            var filePath = ParseFilePath();

            enumerator.Dispose();

            return new ParsedData(ActionType.DeleteFile, dataFile, filePath);
        }

        private string ParseFilePath()
        {
            var result = enumerator.Current;
            enumerator.MoveNext();
            if (!File.Exists(result))
                throw new FileNotFoundException();
            return result;
        }
    }
}
