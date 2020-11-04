using System;
using System.Collections.Generic;
using System.Linq;
using BackupApp.Core.Implementations;

namespace BackupApp
{
    class Program
    {
        static void Main(string[] args) => new ConsoleBackupApp(new[] { "n", "sep", "C:\\Users\\annchous\\Desktop\\testFile.txt", "C:\\Users\\annchous\\Desktop\\testFile2.txt", "|", "backups", "count", "3" }).Run();
    }
}
