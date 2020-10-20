using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    public class MagicCarpet : AirVehicle
    {
        protected override int Speed => 10;

        protected override int DistanceReducer(int distance)
        {
            if (distance < 1000)
                return 100;
            if (distance < 5000)
                return 97;
            if (distance < 10000)
                return 90;
            return 95;
        }
    }
}
