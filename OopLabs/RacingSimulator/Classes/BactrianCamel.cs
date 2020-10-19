using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class BactrianCamel : LandVehicle
    {
        public BactrianCamel() => RestCounter = 0;
        public override int Speed => 10;
        public override int MoveTime => 30;
        public override int RestCounter { get; set; }
        //public override int PassedDistance { get; set; }

        public override double RestDuration()
        {
            if (RestCounter == 0)
                return 5;
            return 8;
        }
    }
}