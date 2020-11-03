using System;
using System.Collections.Generic;
using System.Linq;
using BackupApp.Core.Implementations;

namespace BackupApp
{
    class Program
    {
        static void Main(string[] args) => new ConsoleBackupApp(args).Run();
    }
}
