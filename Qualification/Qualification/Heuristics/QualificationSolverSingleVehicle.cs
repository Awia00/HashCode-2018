using System;
using System.Collections.Generic;
using System.Linq;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification.Heuristics
{
    public class QualificationSolverSingleVehicle : IQualificationSolver
    {
        private readonly QualificationInstance _instance;

        public QualificationSolverSingleVehicle(QualificationInstance instance)
        {
            _instance = instance;
        }
        
        public QualificationResult Solve()
        {
            var rides = new HashSet<Ride>(_instance.Rides);
            var result = new QualificationResult(_instance);

            for (var i = 0; i < _instance.NumberOfVehicles; i++)
            {
                var vehicle = new Vehicle();

                foreach (var ride in ChooseRidesForVehicle(vehicle, rides))
                {
                    result.AddAssignment(vehicle.Id, ride.Ride.Id);
                }
            }

            return result;
        }

        public IEnumerable<(Ride Ride, int Score)> ChooseRidesForVehicle(Vehicle vehicle, IEnumerable<Ride> rides)
        {
            var rideSet = new HashSet<Ride>(rides);
            
            while (rideSet.Any(ride => vehicle.PossiblePickupTime(ride) + ride.Distance <= Math.Min(ride.LatestFinish, _instance.NumberOfSteps)))
            {
                var chosenRide = rideSet
                    .Where(ride => vehicle.PossiblePickupTime(ride) + ride.Distance <= Math.Min(ride.LatestFinish, _instance.NumberOfSteps))
                    .OrderByDescending(ride => ride.Score(_instance, vehicle.TimeAvailable + vehicle.Position.DistanceTo(ride.Start)))
                    .First();

                yield return (chosenRide, chosenRide.Score(_instance, vehicle.TimeAvailable + vehicle.Position.DistanceTo(chosenRide.Start)));
                rideSet.Remove(chosenRide);

                vehicle.Position = chosenRide.End;
                vehicle.TimeAvailable = vehicle.PossiblePickupTime(chosenRide) + chosenRide.Distance;
            }
        }
    }
}