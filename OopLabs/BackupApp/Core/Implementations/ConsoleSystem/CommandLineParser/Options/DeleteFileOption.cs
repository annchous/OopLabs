using System.Collections.Generic;
using System.Linq;
using BackupApp.Core.Abstractions;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser.Options
{
    class DeleteFileOption : Option, IParsable
    {
        public DeleteFileOption(IEnumerable<string> arguments) : base(arguments) {}

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
            return result;
        }
    }
}
