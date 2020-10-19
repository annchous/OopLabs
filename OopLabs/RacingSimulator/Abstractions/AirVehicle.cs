using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    public abstract class AirVehicle : Vehicle
    {
        public override Type VehicleType => typeof(AirVehicle);
        public abstract int PassedDistance { get; set; }
        public abstract int DistanceReducer();
    }
}
