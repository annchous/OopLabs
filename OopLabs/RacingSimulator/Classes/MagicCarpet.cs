using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    public class MagicCarpet : AirVehicle
    {
        public override int Speed => 10;
        public override int PassedDistance { get; set; }

        public override int DistanceReducer()
        {
            if (PassedDistance < 1000)
                return 100;
            if (PassedDistance >= 1000 && PassedDistance < 5000)
                return 97;
            if (PassedDistance >= 5000 && PassedDistance < 10000)
                return 90;
            return 95;
        }
    }
}
