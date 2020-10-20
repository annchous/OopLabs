using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class BactrianCamel : LandVehicle
    {
        protected override int Speed => 10;
        protected override int MoveTime => 30;

        protected override double RestDuration(int number) 
            => number == 0 ? 5 : 8;
    }
}