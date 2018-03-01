using System;
using System.Collections.Generic;
using System.Linq;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification.Heuristics
{
    public class QualificationSolverGreedy : IQualificationSolver
    {
        private readonly QualificationInstance _instance;

        public QualificationSolverGreedy(QualificationInstance instance)
        {
            _instance = instance;
        }
        
        public QualificationResult Solve()
        {
            var vehicles = new List<Vehicle>();
            for (var i = 0; i < _instance.NumberOfVehicles; i++)
            {
                vehicles.Add(new Vehicle());
            }

            Console.Error.WriteLine("Created vehicles");

            return Solve(_instance, vehicles);
        }

        public QualificationResult Solve(QualificationInstance instance, IEnumerable<Vehicle> vehicles)
        {
            var timeQueue = new SortedSet<Vehicle>(vehicles, new VehicleTimeComparer());
            
            var result = new QualificationResult(instance);
            var ridesLeft = instance.Rides.ToList();

            do
            {
                var vehicle = timeQueue.Min;
                timeQueue.Remove(vehicle);

                var pickedRide = ridesLeft
                    .OrderBy(x => x.LatestFinish + 1d/x.Distance + (vehicle.TimeAvailable + x.Start.DistanceTo(vehicle.Position) <= x.EarliestStart ? instance.PerRideBonus : 0))
                    .FirstOrDefault(x => vehicle.TimeAvailable + x.Distance + x.Start.DistanceTo(vehicle.Position) < Math.Min(instance.NumberOfSteps, x.LatestFinish));

                if (pickedRide != null)
                {
                    vehicle.TimeAvailable = Math.Max(vehicle.TimeAvailable + vehicle.Position.DistanceTo(pickedRide.Start), pickedRide.EarliestStart) + pickedRide.Distance;
                    vehicle.Position = pickedRide.End;
                    timeQueue.Add(vehicle);
                    ridesLeft.Remove(pickedRide);
                    result.AddAssignment(vehicle.Id, pickedRide.Id);
                    Console.Error.Write($"\rAssigned ride {pickedRide.Id} to vehicle {vehicle.Id}");
                }
            } while (timeQueue.Any());
            Console.Error.WriteLine();

            return result;
        }
    }
}
