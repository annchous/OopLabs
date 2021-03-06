﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    class Centaur : LandVehicle
    {
        protected override int Speed => 15;
        protected override int MoveTime => 8;

        protected override double RestDuration(int number) => 2;
    }
}
