using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RacingSimulator;

namespace RacingSimulatorTest
{
    [TestClass]
    public class CorrectAnswerTest
    {
        [TestMethod]
        public void SpeedCamelAnswerTest()
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
            var winner = race.RunRace();

            Assert.AreEqual("SpeedCamel", winner.GetType().Name);
        }

        [TestMethod]
        public void BroomAnswerTest()
        {
            var magicCarpet = new MagicCarpet();
            var mortar = new Mortar();
            var broom = new Broom();

            var vehicles = new List<AirVehicle>
            {
                magicCarpet,
                mortar,
                broom
            };

            var race = new Race<AirVehicle>(1000, vehicles);
            var winner = race.RunRace();

            Assert.AreEqual("Broom", winner.GetType().Name);
        }
    }
}
