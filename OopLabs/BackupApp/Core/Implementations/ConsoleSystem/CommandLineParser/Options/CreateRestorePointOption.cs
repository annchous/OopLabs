using System.Collections.Generic;
using System.Linq;
using BackupApp.Core.Abstractions;
using BackupApp.Exceptions;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser.Options
{
    class CreateRestorePointOption : Option, IParsable
    {
        public CreateRestorePointOption(IEnumerable<string> arguments) : base(arguments) {}
        
        public ParsedData Parse()
        {
            if (arguments.Count() != 2)
                throw new WrongArgumentFormat(string.Join(" ", arguments.ToString()));

            var dataFile = ParseDataFile();
            var backupType = ArgumentParser.ParseBackupType(enumerator.Current);
            enumerator.Dispose();
            
            return new ParsedData(ActionType.CreateRestorePoint, dataFile, backupType);
        }
    }
}
