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

            do
            {
                var vehicle = timeQueue.Min;
                timeQueue.Remove(vehicle);

                var endTime = 0;
                var endCoordinate = vehicle.Position;

                // handle vehicle


                if (endTime < instance.NumberOfSteps)
                {
                    vehicle.TimeAvailable = endTime;
                    vehicle.Position = endCoordinate;
                    timeQueue.Add(vehicle);
                }
            } while (timeQueue.Any());

            return null;
        }
    }
}
