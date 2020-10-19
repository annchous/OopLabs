using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    public abstract class Vehicle
    {
        public abstract int Speed { get; }
        public abstract Type VehicleType { get; }
    }
}
