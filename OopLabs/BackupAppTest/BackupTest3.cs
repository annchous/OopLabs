using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupApp.Core.Implementations.ConsoleSystem;
using NUnit.Framework;

namespace BackupAppTest
{
    [TestFixture]
    class BackupTest3
    {
        private ConsoleBackupApp _consoleBackupApp;
        [SetUp]
        public void Setup()
        {
            _consoleBackupApp = new ConsoleBackupApp(new List<string>() { "-n", "-s", "C:\\Users\\annchous\\Desktop\\test.txt", "-df", "backup1", "-c", "2" });
        }

        [Test]
        public void Test1()
        {
            _consoleBackupApp.Run();
            _consoleBackupApp = new ConsoleBackupApp(new List<string>() { "-r", "backup1", "-f" });
            _consoleBackupApp.Run();
            _consoleBackupApp.Run();
        }

        [Test]
        public void Test2()
        {
            DirectoryAssert.Exists("C:\\Users\\annchous\\Desktop\\test.txt_Backup");
        }

        [Test]
        public void Test3()
        {
            FileAssert.Exists("C:\\Users\\annchous\\Desktop\\test.txt_Backup\\RestorePoint_1");
            FileAssert.Exists("C:\\Users\\annchous\\Desktop\\test.txt_Backup\\RestorePoint_2");
        }

        [Test]
        public void Test4()
        {
            FileAssert.DoesNotExist("C:\\Users\\annchous\\Desktop\\test.txt_Backup\\RestorePoint_0");
        }
    }
}
