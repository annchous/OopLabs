using System;
using System.Collections.Generic;

namespace RacingSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var magicCarpet = new MagicCarpet();
            var mortar = new Mortar();
            var broom = new Broom();
            var camel = new BactrianCamel();
            var speedCamel = new SpeedCamel();
            var centaur = new Centaur();
            var allTerrainBoots = new AllTerrainBoots();

            var vehicles = new List<Vehicle>
            {
                magicCarpet,
                mortar,
                broom,
                camel,
                speedCamel,
                centaur,
                allTerrainBoots
            };

            var race = new Race<Vehicle>(1000, vehicles);
            Console.WriteLine(race.Run().GetType().Name);
        }
    }
}
