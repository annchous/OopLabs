using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Abstractions;

namespace BackupApp.CommandLineParser.Options
{
    class InfoOption : Option, IParsable
    {
        public InfoOption(IEnumerable<string> arguments) : base(arguments) {}

        public ParsedData Parse()
        {
            CheckArguments(1);
            var dataFile = ParseDataFile();
            enumerator.Dispose();

            return new ParsedData(ActionType.Info, dataFile);
        }
    }
}
