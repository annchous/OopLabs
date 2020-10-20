using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class BactrianCamel : LandVehicle
    {
        protected override int Speed => 10;
        protected override int MoveTime => 30;
        protected override int RestCounter { get; set; }

        protected override double RestDuration() 
            => RestCounter == 0 ? 5 : 8;
    }
}