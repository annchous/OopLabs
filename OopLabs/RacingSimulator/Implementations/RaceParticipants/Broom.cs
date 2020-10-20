using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class Broom : AirVehicle
    {
        protected override int Speed => 20;
        protected override int DistanceReducer(int distance) 
            => 100 - (int)(distance / 1000.0);
    }
}
