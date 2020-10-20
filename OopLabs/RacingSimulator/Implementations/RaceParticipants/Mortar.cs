using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class Mortar : AirVehicle
    {
        protected override int Speed => 8;
        protected override int DistanceReducer(int distance) => 94;
    }
}
