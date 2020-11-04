using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class SizeAlgorithm : Algorithm
    {
        private long _size;
        public SizeAlgorithm(long size) : base(AlgorithmType.Size)
        {
            _size = size;
        }

        public override int Calculate(ref Backup backup)
        {
            throw new NotImplementedException();
        }
    }
}
