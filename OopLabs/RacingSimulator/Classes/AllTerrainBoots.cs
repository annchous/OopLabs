using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class AllTerrainBoots : LandVehicle
    {
        public override int Speed => 6;
        public override int RestCounter { get; set; }
        public override int MoveTime => 60;
        public override double RestDuration()
        {
            if (RestCounter == 0)
                return 10;
            return 5;
        }
    }
}
