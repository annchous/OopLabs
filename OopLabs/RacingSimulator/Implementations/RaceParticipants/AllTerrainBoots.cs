using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class AllTerrainBoots : LandVehicle
    {
        protected override int Speed => 6;
        protected override int MoveTime => 60;

        protected override double RestDuration(int number)
        {
            if (number == 0)
                return 10;
            return 5;
        }
    }
}
