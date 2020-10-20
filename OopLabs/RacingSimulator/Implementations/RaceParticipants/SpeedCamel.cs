using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class SpeedCamel : LandVehicle
    {
        protected override int Speed => 40;
        protected override int RestCounter { get; set; }
        protected override int MoveTime => 10;

        protected override double RestDuration()
        {
            if (RestCounter == 0)
                return 5;
            if (RestCounter == 1)
                return 6.5;
            return 8;
        }
    }
}
