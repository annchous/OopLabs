using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp
{
    class Program
    {
        //static void Main(string[] args) => new ConsoleBackupApp(new[] { "n", "sep", "C:\\Users\\annchous\\Desktop\\testFile.txt", "C:\\Users\\annchous\\Desktop\\testFile2.txt", "|", "backups", "count", "3" }).Run();
        static void Main(string[] args) => new ConsoleBackupApp(new[] { "n", "com", "C:\\Users\\annchous\\Desktop\\Backups", "C:\\Users\\annchous\\Desktop\\testFile.txt", "C:\\Users\\annchous\\Desktop\\testFile2.txt", "|", "backups", "count", "5" }).Run();
        //static void Main(string[] args) => new ConsoleBackupApp(new[] { "e", "backups", "full" }).Run();
        //static void Main(string[] args) => new ConsoleBackupApp(new[] { "e", "backups", "incremental" }).Run();
    }
}
