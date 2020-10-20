using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class SpeedCamel : LandVehicle
    {
        protected override int Speed => 40;
        protected override int MoveTime => 10;

        protected override double RestDuration(int number)
        {
            if (number == 0)
                return 5;
            if (number == 1)
                return 6.5;
            return 8;
        }
    }
}
