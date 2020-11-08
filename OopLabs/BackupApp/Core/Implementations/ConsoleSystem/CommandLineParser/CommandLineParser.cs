using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser.Options;

namespace BackupApp.Core.Implementations.ConsoleSystem.CommandLineParser
{
    public enum ActionType
    {
        CreateBackup,
        CreateRestorePoint,
        DeleteFile,
        AddFile,
        ConfigurationInfo
    }

    class CommandLineParser : IParsable
    {
        private readonly IEnumerable<string> arguments;
        public CommandLineParser(IEnumerable<string> arguments)
        {
            this.arguments = arguments;
        }
        public ParsedData Parse()
        {
            return ArgumentParser.ParseAction(arguments.ElementAt(0)) switch
            {
                ActionType.CreateBackup => new CreateBackupOption(arguments.Skip(1)).Parse(),
                ActionType.CreateRestorePoint => new CreateRestorePointOption(arguments.Skip(1)).Parse(),
                ActionType.DeleteFile => new DeleteFileOption(arguments.Skip(1)).Parse(),
                ActionType.ConfigurationInfo => new ShowConfigurationInfoOption(arguments.Skip(1)).Parse(),
                _ => null
            };
        }
    }
}
