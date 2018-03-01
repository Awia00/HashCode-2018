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

            var result = new QualificationResult(_instance);
            
            foreach (var assignment in Solve(vehicles, _instance.Rides))
            {
                result.AddAssignment(assignment.VehicleId, assignment.RideId);
            }
            
            return result;
        }

        public IEnumerable<(int VehicleId, int RideId, int Score)> Solve(IEnumerable<Vehicle> vehicles, IEnumerable<Ride> rides)
        {
            var timeQueue = new SortedSet<Vehicle>(vehicles, new VehicleTimeComparer());
            
            var ridesLeft = rides.ToList();

            do
            {
                var vehicle = timeQueue.Min;
                timeQueue.Remove(vehicle);

                var pickedRide = ridesLeft
                    .OrderBy(x => x.LatestFinish + 1d/x.Distance + (vehicle.TimeAvailable + x.Start.DistanceTo(vehicle.Position) <= x.EarliestStart ? _instance.PerRideBonus : 0))
                    .FirstOrDefault(x => vehicle.PossiblePickupTime(x) + x.Distance < Math.Min(_instance.NumberOfSteps, x.LatestFinish));

                if (pickedRide != null)
                {
                    yield return (vehicle.Id, pickedRide.Id, pickedRide.Score(_instance, vehicle.PossiblePickupTime(pickedRide)));
                    
                    vehicle.TimeAvailable = vehicle.PossiblePickupTime(pickedRide)  + pickedRide.Distance;
                    vehicle.Position = pickedRide.End;
                    timeQueue.Add(vehicle);
                    ridesLeft.Remove(pickedRide);
                    Console.Error.Write($"\rAssigned ride {pickedRide.Id} to vehicle {vehicle.Id}");
                }
            } while (timeQueue.Any());
            Console.Error.WriteLine();
        }
    }
}
