using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class Broom : AirVehicle
    {
        public override int Speed => 20;
        public override int PassedDistance { get; set; }
        public override int DistanceReducer() => 100 - (int)(PassedDistance / 1000.0);
    }
}
