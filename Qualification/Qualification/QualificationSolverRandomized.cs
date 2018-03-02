using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windemann.HashCode.Qualification.Heuristics;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public class QualificationSolverRandomized
    {
        private QualificationInstance _instance;
        private QualificationSolverGreedy solver;
        public QualificationSolverRandomized(QualificationInstance instance)
        {
            solver = new QualificationSolverGreedy(instance);
            _instance = instance;
        }
        public QualificationResult Solve()
        {
            var vehicles = new List<Vehicle>();
            var conflicts = new Dictionary<int, HashSet<int>>();
            for (var i = 0; i < _instance.NumberOfVehicles; i++)
            {
                vehicles.Add(new Vehicle());
                conflicts.Add(i, new HashSet<int>());
            }

            
            var rand = new Random();
            var result = new QualificationResult(_instance);
            for (var i = 0; i < 10; i++)
            {
                var partial = solver.Solve(vehicles, _instance.Rides, conflicts).ToList();
                var vehiclesWithoutRides = vehicles.OrderBy(x => partial.Count(y => y.VehicleId==x.Id)).Take(vehicles.Count/10);
                foreach (var vehicleWithoutRides in vehiclesWithoutRides)
                {
                    var randCoord = new Coordinate {
                        Row = rand.Next(_instance.Rows),
                        Column = rand.Next(_instance.Columns)
                    };
                    var time = vehicleWithoutRides.Position.DistanceTo(randCoord);
                    vehicleWithoutRides.Position = randCoord;
                    vehicleWithoutRides.TimeAvailable = time;
                }
                var partialResult = new QualificationResult(_instance);
                foreach (var valueTuple in partial)
                {
                    partialResult.AddAssignment(valueTuple.VehicleId, valueTuple.RideId, valueTuple.Score);
                }

                if (partialResult.Score > result.Score)
                {
                    Console.WriteLine($"Randomized improved score from: {result.Score} to {partialResult.Score}");
                    result = partialResult;
                }
            }

            return result;
        }
    }
}
