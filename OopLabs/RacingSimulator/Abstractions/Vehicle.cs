using System;
using System.Collections.Generic;
using System.Text;

namespace RacingSimulator
{
    public abstract class Vehicle
    {
        protected abstract int Speed { get; }
        public abstract double Run(int distance);
    }
}
