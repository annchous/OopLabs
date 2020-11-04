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
    class CountAlgorithm : Algorithm
    {
        private readonly int _count;

        public CountAlgorithm(int count) : base(AlgorithmType.Count)
        {
            _count = count;
        }

        // TODO: fix
        public override int Calculate(ref Backup backup)
        {
            var unwantedPointsCount = backup.RestorePoints.Count - _count > 0 
                ? backup.RestorePoints.Count - _count 
                : 0;
            var pointsToSave = new List<RestorePoint>();
            var count = backup.RestorePoints.Count >= _count ? _count : 0;
            pointsToSave.AddRange(backup.RestorePoints.GetRange(unwantedPointsCount, count));

            while (Warning(pointsToSave.FirstOrDefault()) && unwantedPointsCount > 0)
            {
                unwantedPointsCount--;
                pointsToSave.Insert(0, backup.RestorePoints[unwantedPointsCount - 1]);
            }

            return unwantedPointsCount;
        }

        public override void Clean(ref Backup backup)
        {
            var unwantedPointsCount = Calculate(ref backup);
            for (int i = 0; i < unwantedPointsCount; i++)
            {
                if (backup.RestorePoints[i].GetType() == typeof(FullRestorePoint))
                {
                    if (Directory.Exists(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath)))
                        Directory.Delete(Path.GetDirectoryName(backup.RestorePoints[i].RestorePointPath), true);
                }

                else if (File.Exists(backup.RestorePoints[i].RestorePointPath))
                    File.Delete(backup.RestorePoints[i].RestorePointPath);
            }

            backup.RestorePoints.RemoveRange(0, unwantedPointsCount);
        }

        private bool Warning(RestorePoint restorePoint)
        {
            if (restorePoint.GetType() != typeof(IncrementalRestorePoint)) return false;
            Console.WriteLine("Warning: to implement the algorithm for cleaning restore points, you must store one more point.");
            return true;
        }
    }
}
