using System.Collections.Generic;
using BackupApp.Core.Abstractions;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser.Options
{
    class ShowConfigurationInfoOption : Option, IParsable
    {
        public ShowConfigurationInfoOption(IEnumerable<string> arguments) : base(arguments) {}
        public ParsedData Parse()
        {
            var dataFile = ParseDataFile();
            enumerator.Dispose();

            return new ParsedData(ActionType.ConfigurationInfo, dataFile);
        }
    }
}
