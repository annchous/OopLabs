using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    public abstract class AirVehicle : Vehicle
    {
        protected abstract int DistanceReducer(int distance);
        public override double Run(int distance) 
            => ((DistanceReducer(distance) / 100.0) * distance) / Speed;
    }
}
