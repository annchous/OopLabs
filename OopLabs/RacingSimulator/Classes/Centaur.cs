using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class Centaur : LandVehicle
    {
        public override int Speed => 15;
        public override int RestCounter { get; set; }
        public override int MoveTime => 8;
        public override double RestDuration() => 2;
    }
}
