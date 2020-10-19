using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class Mortar : AirVehicle
    {
        public override int Speed => 8;
        public override int PassedDistance { get; set; }
        public override int DistanceReducer() => 94;
    }
}
