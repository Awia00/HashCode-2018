using System;
using System.Collections.Generic;
using System.Linq;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    class QualificationSolverGreedy : IQualificationSolver
    {
        public QualificationResult Solve(QualificationInstance instance)
        {
            var timeQueue = new SortedSet<Vehicle>(new VehicleTimeComparer());
            for (var i = 0; i < instance.NumberOfVehicles; i++)
            {
                timeQueue.Add(new Vehicle());
            }

            Console.Error.WriteLine("Created vehicles");

            var result = new QualificationResult();
            var ridesLeft = instance.Rides.ToList();

            do
            {
                var vehicle = timeQueue.Min;
                timeQueue.Remove(vehicle);

                var pickedRide = ridesLeft.OrderBy(x => x.LatestFinish + x.Distance + x.Start.DistanceTo(vehicle.Position)).FirstOrDefault(x => vehicle.TimeAvailable + x.Distance + x.Start.DistanceTo(vehicle.Position) < instance.NumberOfSteps);

                if (pickedRide != null)
                {
                    vehicle.TimeAvailable = vehicle.TimeAvailable + pickedRide.Distance + vehicle.Position.DistanceTo(pickedRide.Start);
                    vehicle.Position = pickedRide.End;
                    timeQueue.Add(vehicle);
                    ridesLeft.Remove(pickedRide);
                    result.AddAssignment(vehicle.Id, pickedRide.Id);
                    Console.Error.WriteLine($"Assigned ride {pickedRide.Id} to vehicle {vehicle.Id}");
                }
            } while (timeQueue.Any());

            return result;
        }
    }
}
