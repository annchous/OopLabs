using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp
{
    class Program
    {
        //static void Main(string[] args) => new ConsoleBackupApp(args).Run();
        static void Main(string[] args) => new ConsoleBackupApp(new []{ "n", "sep", "C:\\Users\\annchous\\Desktop\\testFile.txt", "C:\\Users\\annchous\\Desktop\\testFile2.txt", "|", "backups", "count", "3" }).Run();
    }
}
