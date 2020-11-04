using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;
using BackupApp.Core.Implementations.RestorePointSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    [Serializable]
    class DateAlgorithm : Algorithm
    {
        private DateTime _date;
        public DateAlgorithm(DateTime date) : base(AlgorithmType.Date)
        {
            _date = date;
        }

        public override int Calculate(ref Backup backup)
        {
            var unwantedPointsCount = backup.RestorePoints.Count(x => x.Date < _date);
            var pointsToSave = new List<RestorePoint>();
            var count = backup.RestorePoints.Count - unwantedPointsCount > 0 ? backup.RestorePoints.Count - unwantedPointsCount : 0;
            pointsToSave.AddRange(backup.RestorePoints.GetRange(unwantedPointsCount, count));

            while (pointsToSave.FirstOrDefault() != null && Warning(pointsToSave.FirstOrDefault()) && unwantedPointsCount > 0)
            {
                unwantedPointsCount--;
                pointsToSave.Insert(0, backup.RestorePoints[unwantedPointsCount - 1]);
            }

            return unwantedPointsCount;
        }

        private bool Warning(RestorePoint restorePoint)
        {
            if (restorePoint.GetType() != typeof(IncrementalRestorePoint)) return false;
            Console.WriteLine("Warning: to implement the algorithm for cleaning restore points, you must store one more point.");
            return true;
        }
    }
}
