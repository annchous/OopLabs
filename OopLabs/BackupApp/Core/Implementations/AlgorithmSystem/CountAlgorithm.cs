using System;
using BackupApp.Core.Abstractions;
using BackupApp.Core.Implementations.BackupSystem;

namespace BackupApp.Core.Implementations.AlgorithmSystem
{
    [Serializable]
    class CountAlgorithm : Algorithm
    {
        private readonly int _count;

        public CountAlgorithm(int count)
        {
            _count = count;
        }

        protected override int UnwantedPointsCount(Backup backup) => backup.RestorePoints.Count - _count > 0
                ? backup.RestorePoints.Count - _count
                : 0;

        protected override int PointsToSaveCount(Backup backup) => backup.RestorePoints.Count >= _count ? _count : 0;
    }
}
