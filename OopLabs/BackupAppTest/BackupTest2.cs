using System;
using System.Collections.Generic;
using BackupApp.Core.Implementations.ConsoleSystem;
using NUnit.Framework;

namespace BackupAppTest
{
    [TestFixture]
    class BackupTest2
    {
        private ConsoleBackupApp _consoleBackupApp;
        [SetUp]
        public void SetUp()
        {
            _consoleBackupApp = new ConsoleBackupApp(new List<string>() { "-n", "-s", "C:\\Users\\annchous\\Desktop\\notExistingFile.txt", "-df", "backup", "-c", "3" });
        }

        [Test]
        public void Test1()
        {
            Assert.Throws<BackupApp.Exception.FileNotFoundException>(() => _consoleBackupApp.Run());
        }
    }
}
