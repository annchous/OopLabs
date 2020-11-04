using System;
using System.Collections.Generic;
using System.Linq;
using BackupApp.Core.Implementations;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp
{
    class Program
    {
        //static void Main(string[] args) => new ConsoleBackupApp(new[] { "n", "sep", "C:\\Users\\annchous\\Desktop\\testFile.txt", "C:\\Users\\annchous\\Desktop\\testFile2.txt", "|", "backups", "count", "3" }).Run();
        //static void Main(string[] args) => new ConsoleBackupApp(new[] { "n", "com", "C:\\Users\\annchous\\Desktop\\Backups", "C:\\Users\\annchous\\Desktop\\testFile.txt", "C:\\Users\\annchous\\Desktop\\testFile2.txt", "|", "backups", "date", "03.11.2020" }).Run();
        static void Main(string[] args) => new ConsoleBackupApp(new[] { "e", "backups", "full" }).Run();
    }
}
