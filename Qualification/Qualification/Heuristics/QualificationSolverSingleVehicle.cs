using System;
using System.Collections.Generic;
using System.Linq;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification.Heuristics
{
    public class QualificationSolverSingleVehicle : IQualificationSolver
    {
        public QualificationResult Solve(QualificationInstance instance)
        {
            var rides = new HashSet<Ride>(instance.Rides);
            var result = new QualificationResult(instance);

            for (var i = 0; i < instance.NumberOfVehicles; i++)
            {
                var vehicle = new Vehicle();

                while (rides.Any(ride => vehicle.PossiblePickupTime(ride) + ride.Distance <= Math.Min(ride.LatestFinish, instance.NumberOfSteps)))
                {
                    var chosenRide = rides
                        .Where(ride => vehicle.PossiblePickupTime(ride) + ride.Distance <= Math.Min(ride.LatestFinish, instance.NumberOfSteps))
                        .OrderByDescending(ride => ride.Score(instance, vehicle.TimeAvailable + vehicle.Position.DistanceTo(ride.Start)))
                        .First();

                    result.AddAssignment(vehicle.Id, chosenRide.Id);
                    rides.Remove(chosenRide);

                    vehicle.Position = chosenRide.End;
                    vehicle.TimeAvailable = vehicle.PossiblePickupTime(chosenRide) + chosenRide.Distance;
                }
            }

            return result;
        }
    }
}