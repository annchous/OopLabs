using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    abstract class LandVehicle : Vehicle
    {
        public override Type VehicleType => typeof(LandVehicle);
        //public abstract int PassedDistance { get; set; }
        public abstract int RestCounter { get; set; }
        public abstract int MoveTime { get; }
        public abstract double RestDuration();
    }
}
