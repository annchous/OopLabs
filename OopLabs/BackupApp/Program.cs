﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp
{
    class Program
    {
        static void Main(string[] args) => new ConsoleBackupApp(args.ToList()).Run();
    }
}