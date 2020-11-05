using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp
{
    class Program
    {
        static void Main(string[] args) => new ConsoleBackupApp(args).Run();
       
    }
}
