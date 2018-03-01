using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification
{
    public class QualificationResult
    {
        // Maps from vehicle id to ride ids.
        public Dictionary<int, List<int>> Assignments { get; }

        public QualificationResult()
        {
            Assignments = new Dictionary<int, List<int>>();
        }

        public QualificationResult(Dictionary<int, List<int>> assignments)
        {
            Assignments = assignments;
        }

        public void AddAssignment(int vehicleId, List<int> rideIds)
        {
            Assignments.Add(vehicleId, rideIds);
        }

        public void AddAssignment(int vehicleId, int rideId)
        {
            
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