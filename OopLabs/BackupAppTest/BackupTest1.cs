using System.Collections.Generic;
using BackupApp.Core.Implementations.ConsoleSystem;
using NUnit.Framework;

namespace BackupAppTest
{
    [TestFixture]
    public class BackupTest1
    {
        private ConsoleBackupApp _consoleBackupApp;
        [SetUp]
        public void Setup()
        {
            _consoleBackupApp = new ConsoleBackupApp(new List<string>() {"-n", "-s", "C:\\Users\\annchous\\Desktop\\testFile.txt", "C:\\Users\\annchous\\Desktop\\testFile2.txt", "-df", "backup", "-c", "3"});
        }

        [Test]
        public void Test1()
        {
            _consoleBackupApp.Run();
        }

        [Test]
        public void Test2()
        {
            DirectoryAssert.Exists("C:\\Users\\annchous\\Desktop\\testFile.txt_Backup");
            DirectoryAssert.Exists("C:\\Users\\annchous\\Desktop\\testFile2.txt_Backup");
        }

        [Test]
        public void Test3()
        {
            FileAssert.Exists("C:\\Users\\annchous\\Desktop\\testFile.txt_Backup\\RestorePoint_0");
            FileAssert.Exists("C:\\Users\\annchous\\Desktop\\testFile2.txt_Backup\\RestorePoint_0");
        }
    }
}