using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    abstract class LandVehicle : Vehicle
    {
        protected abstract int RestCounter { get; set; }
        protected abstract int MoveTime { get; }
        protected abstract double RestDuration();
        public override double Run(int distance)
        {
            var time = (double)distance / Speed;
            var restCount = (int)time / MoveTime;
            for (int i = 0; i < restCount; i++)
            {
                time += RestDuration();
                RestCounter++;
            }

            return time;
        }
    }
}
