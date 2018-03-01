using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification.Model
{
    public class QualificationInstance
    {
        public int Rows { get; }
        public int Columns { get; }
        public int NumberOfVehicles { get; }
        public int NumberOfRides { get; }
        public int PerRideBonus { get; }
        public int NumberOfSteps { get; }
        public List<Ride> Rides { get; }

        public QualificationInstance(int rows, int columns, int numberOfVehicles, int numberOfRides, int perRideBonus, int numberOfSteps, List<Ride> rides)
        {
            Rows = rows;
            Columns = columns;
            NumberOfVehicles = numberOfVehicles;
            NumberOfRides = numberOfRides;
            PerRideBonus = perRideBonus;
            NumberOfSteps = numberOfSteps;
            Rides = rides;
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{Rows} {Columns} {NumberOfVehicles} {NumberOfRides} {PerRideBonus} {NumberOfSteps}");
            
            foreach (var ride in Rides)
            {
                builder.AppendLine($"{ride.Start.Row} {ride.Start.Column} {ride.End.Row} {ride.End.Column} {ride.EarliestStart} {ride.LatestFinish}");
            }

            return builder.ToString();
        }
    }
}