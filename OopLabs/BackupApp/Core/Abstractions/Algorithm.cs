using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp.Core.Abstractions
{
    public abstract class Algorithm
    {
        public AlgorithmType Type { get; }

        public Algorithm(AlgorithmType algorithmType)
        {
            Type = algorithmType;
        }

        public abstract int Calculate(ref Backup backup);
        public abstract void Clean(ref Backup backup);
    }
}
