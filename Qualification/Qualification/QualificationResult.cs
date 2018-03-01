using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification
{
    public class QualificationResult
    {
        public List<List<int>> Assignments { get; }

        public QualificationResult()
        {
            Assignments = new List<List<int>>();
        }

        public QualificationResult(List<List<int>> assignments)
        {
            Assignments = assignments;
        }

        public void AddAssignment(List<int> rideIds)
        {
            Assignments.Add(rideIds);
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();
            
            foreach (var assignment in Assignments)
            {
                builder.Append(assignment.Count);

                foreach (var rideId in assignment)
                {
                    builder.Append(" ").Append(rideId);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}