using System.Collections.Generic;
using System.Text;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public class QualificationResult
    {
        // Maps from vehicle id to ride ids.
        public Dictionary<int, List<int>> Assignments { get; }

        public int Score { get; set; }

        public QualificationResult(QualificationInstance instance)
        {
            Assignments = new Dictionary<int, List<int>>();

            for (var ride = 0; ride < instance.NumberOfVehicles; ++ride)
            {
                Assignments.Add(ride, new List<int>());
            }
        }

        public void AddAssignment(int vehicleId, int rideId)
        {
            Assignments[vehicleId].Add(rideId);
        }

        public void AddAssignment(int vehicleId, int rideId, int score)
        {
            Assignments[vehicleId].Add(rideId);
            Score += score;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            
            foreach (var assignment in Assignments)
            {
                builder.Append(assignment.Value.Count);

                foreach (var rideId in assignment.Value)
                {
                    builder.Append(" ").Append(rideId);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}