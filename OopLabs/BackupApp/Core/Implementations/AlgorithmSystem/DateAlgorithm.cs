using System;
using System.Linq;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    [Serializable]
    class DateAlgorithm : Algorithm
    {
        private DateTime _date;
        public DateAlgorithm(DateTime date)
        {
            _date = date;
        }

        protected override int UnwantedPointsCount(Backup backup) => backup.RestorePoints.Count(x => x.CreationTime < _date);

        protected override int PointsToSaveCount(Backup backup) => backup.RestorePoints.Count - UnwantedPointsCount(backup) > 0 
            ? backup.RestorePoints.Count - UnwantedPointsCount(backup) 
            : 0;
    }
}
