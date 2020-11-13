using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupApp.CommandLineParser.Options;
using BackupApp.Core.Abstractions;
using BackupApp.Exception;

namespace BackupApp.CommandLineParser
{
    public static class ActionTypeExtensions
    {
        public static IParseable ConvertToParseable(this ActionType actionType, IEnumerable<string> arguments)
        {
            return actionType switch
            {
                ActionType.CreateBackupSystem => new CreateBackupSystemOption(arguments.Skip(1)),
                ActionType.CreateRestore => new CreateRestoreOption(arguments.Skip(1)),
                ActionType.DeleteBackup => new DeleteBackupOption(arguments.Skip(1)),
                ActionType.AddBackup => new AddBackupOption(arguments.Skip(1)),
                ActionType.Info => new InfoOption(arguments.Skip(1)),
                _ => throw new UnknownArgumentException(arguments.ElementAt(0))
            };
        }

    }
}
