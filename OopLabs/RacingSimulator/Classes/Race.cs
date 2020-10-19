using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RacingSimulator
{
    class Race<T> where T: Vehicle
    {
        private int Distance { get; }
        private List<T> ParticipantsList { get; }
        private Dictionary<Vehicle, double> ElapsedTime { get; }
        public Race(int distance, List<T> participantsList)
        {
            Distance = distance;
            ParticipantsList = participantsList;
            ElapsedTime = new Dictionary<Vehicle, double>();
        }

        public Vehicle Run()
        {
            foreach (var participant in ParticipantsList)
            {
                ElapsedTime.Add(participant, 0);
                RunVehicle(ElapsedTime.Last());
            }

            return FindMinTime();
        }

        private void RunVehicle(KeyValuePair<Vehicle, double> vehicle)
        {
            if (vehicle.Key.VehicleType == typeof(AirVehicle))
                RunAirVehicle(vehicle);
            else
                RunLandVehicle(vehicle);
        }

        private void RunAirVehicle(KeyValuePair<Vehicle, double> vehicle)
        {
            var airVehicle = (AirVehicle) vehicle.Key;
            var remainingDistance = Distance;
            
            while (remainingDistance > 0)
            {
                remainingDistance -= airVehicle.Speed;
                airVehicle.PassedDistance += airVehicle.Speed;
                remainingDistance = (int) (remainingDistance * (airVehicle.DistanceReducer() / 100.0));
                ElapsedTime[vehicle.Key]++;
            }
        }

        private void RunLandVehicle(KeyValuePair<Vehicle, double> vehicle)
        {
            var landVehicle = (LandVehicle)vehicle.Key;
            var remainingDistance = Distance;

            while (remainingDistance > 0)
            {
                for (int i = 0; i < landVehicle.MoveTime; i++)
                {
                    if (remainingDistance <= 0)
                        break;
                    remainingDistance -= landVehicle.Speed;
                    ElapsedTime[vehicle.Key]++;
                }

                if (remainingDistance > 0)
                    ElapsedTime[vehicle.Key] += landVehicle.RestDuration();
                landVehicle.RestCounter++;
            }
        }

        private Vehicle FindMinTime()
        {
            var minTime = double.MaxValue;
            Vehicle result = null;
            foreach (var participant in ElapsedTime)
            {
                if (participant.Value < minTime)
                {
                    minTime = participant.Value;
                    result = participant.Key;
                }
            }

            return result;
        }
    }
}
