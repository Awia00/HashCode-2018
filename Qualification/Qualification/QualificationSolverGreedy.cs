using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
                }
            } while (timeQueue.Any());

            return null;
        }
    }

    class VehicleTimeComparer : IComparer<Vehicle>
    {
        public int Compare(Vehicle x, Vehicle y)
        {
            return x.TimeAvailable <= y.TimeAvailable ? -1 : 1;
        }
    }
}
