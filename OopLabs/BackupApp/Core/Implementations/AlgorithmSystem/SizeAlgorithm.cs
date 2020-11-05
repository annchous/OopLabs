using System;
using System.Collections.Generic;
using System.Linq;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.ConsoleSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    [Serializable]
    class SizeAlgorithm : Algorithm
    {
        private readonly long _size;
        public SizeAlgorithm(long size) : base(AlgorithmType.Size)
        {
            _size = size;
        }

        public override int Calculate(Backup backup)
        {
            var unwantedPointsCount = backup.RestorePoints.Count - PointsToSaveCount(backup) > 0 
                    ? backup.RestorePoints.Count - PointsToSaveCount(backup) 
                    : 0;
            var pointsToSave = new List<RestorePoint>();
            pointsToSave.AddRange(backup.RestorePoints.GetRange(unwantedPointsCount, PointsToSaveCount(backup)));

            while (pointsToSave.FirstOrDefault() != null && Warning(pointsToSave.FirstOrDefault()) && unwantedPointsCount > 0)
            {
                pointsToSave.Insert(0, backup.RestorePoints[unwantedPointsCount - 1]);
                unwantedPointsCount--;
            }

            return unwantedPointsCount;
        }

        private int PointsToSaveCount(Backup backup)
        {
            long sum = 0;
            var count = 0;
            var reversedPointsList = backup.RestorePoints;
            reversedPointsList.Reverse();
            foreach (var point in reversedPointsList.TakeWhile(point => sum + point.Size <= _size))
            {
                sum += point.Size;
                count++;
            }

            return count;
        }
    }
}
