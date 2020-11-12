using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackupApp.Core.Implementations.BackupSystem;
using BackupApp.Core.Implementations.Logger;
using BackupApp.Core.Implementations.RestorePointSystem;

namespace BackupApp.Core.Abstractions
{
    [Serializable]
    public abstract class Algorithm
    {
        public void Clean(Backup backup)
        {
            var unwantedPointsCount = Calculate(backup);
            for (int i = 0; i < unwantedPointsCount; i++)
                backup.DeleteRestorePoint(backup.RestorePoints[i]);

            backup.RestorePoints.RemoveRange(0, unwantedPointsCount);
        }

        public virtual int Calculate(Backup backup)
        {
            var unwantedPointsCount = UnwantedPointsCount(backup);
            var pointsToSave = new List<RestorePoint>();
            pointsToSave.AddRange(backup.RestorePoints.GetRange(unwantedPointsCount, PointsToSaveCount(backup)));

            while (pointsToSave.FirstOrDefault() != null
                   && NeedMorePointsToSave(pointsToSave.FirstOrDefault())
                   && unwantedPointsCount > 0)
            {
                pointsToSave.Insert(0, backup.RestorePoints[unwantedPointsCount - 1]);
                unwantedPointsCount--;
            }

            return unwantedPointsCount;
        }

        protected bool NeedMorePointsToSave(RestorePoint restorePoint)
        {
            if (restorePoint is FullRestorePoint) return false;
            new BackupLogger().Warning("To implement the algorithm for cleaning restore points, you must store one more point.");
            return true;
        }

        protected abstract int UnwantedPointsCount(Backup backup);
        protected abstract int PointsToSaveCount(Backup backup);
    }
}
