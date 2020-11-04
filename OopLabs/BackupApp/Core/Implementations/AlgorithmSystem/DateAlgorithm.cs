using System;
using System.Collections.Generic;
using System.Text;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    class DateAlgorithm : Algorithm
    {
        private DateTime _date;
        public DateAlgorithm(DateTime date) : base(AlgorithmType.Date)
        {
            _date = date;
        }

        public override int Calculate(ref Backup backup)
        {
            throw new NotImplementedException();
        }

        public override void Clean(ref Backup backup)
        {
            throw new NotImplementedException();
        }
    }
}
