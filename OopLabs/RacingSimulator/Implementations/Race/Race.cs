using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RacingSimulator
{
    class Race<T> where T: Vehicle
    {
        public int Distance { get; }
        private List<T> ParticipantsList { get; }
        public Race(int distance, List<T> participantsList)
        {
            Distance = distance;
            ParticipantsList = participantsList;
        }

        public Vehicle RunRace()
        {
            var minTime = double.MaxValue;
            Vehicle winner = null;
            
            foreach (var participant in ParticipantsList)
            {
                double time;
                if ((time = participant.Run(Distance)) < minTime)
                {
                    minTime = time;
                    winner = participant;
                }

                Console.WriteLine($"Participant: {participant.GetType().Name}\nTime: {time}\n");
            }

            return winner;
        }
    }
}
