using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackupApp.CommandLineParser.Options;
using BackupApp.Core.Abstractions;
using BackupApp.Exception;

namespace BackupApp.CommandLineParser
{
    class Parser : IParsable
    {
        private readonly IEnumerable<string> _arguments;
        public Parser(IEnumerable<string> arguments)
        {
            this._arguments = arguments;
        }
        public ParsedData Parse()
        {
            return StaticParser.ParseAction(_arguments.ElementAt(0)) switch
            {
                ActionType.CreateBackupSystem => new CreateBackupSystemOption(_arguments.Skip(1)).Parse(),
                ActionType.CreateRestore => new CreateRestoreOption(_arguments.Skip(1)).Parse(),
                ActionType.DeleteBackup => new DeleteBackupOption(_arguments.Skip(1)).Parse(),
                ActionType.AddBackup => new AddBackupOption(_arguments.Skip(1)).Parse(),
                ActionType.Info => new InfoOption(_arguments.Skip(1)).Parse(),
                _ => throw new UnknownArgumentException(_arguments.ElementAt(0))
            };
        }
    }
}
