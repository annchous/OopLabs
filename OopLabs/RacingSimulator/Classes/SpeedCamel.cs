using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class SpeedCamel : LandVehicle
    {
        public override int Speed => 40;
        public override int RestCounter { get; set; }
        public override int MoveTime => 10;
        public override double RestDuration()
        {
            if (RestCounter == 0)
                return 5;
            if (RestCounter == 1)
                return 6.5;
            return 8;
        }
    }
}
