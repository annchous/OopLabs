using System;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    [Serializable]
    class SizeAlgorithm : Algorithm
    {
        private readonly long _size;
        public SizeAlgorithm(long size)
        {
            _size = size;
        }

        protected override int UnwantedPointsCount(Backup backup) => backup.RestorePoints.Count - PointsToSaveCount(backup) > 0
            ? backup.RestorePoints.Count - PointsToSaveCount(backup)
            : 0;

        protected override int PointsToSaveCount(Backup backup)
        {
            long sum = 0;
            var count = 0;
            var reversedPointsList = backup.RestorePoints;
            reversedPointsList.Reverse();
            foreach (var point in reversedPointsList)
            {
                if (sum + Math.Abs(point.Size) > _size) break;
                sum += point.Size;
                count++;
            }

            return count;
        }
    }
}
