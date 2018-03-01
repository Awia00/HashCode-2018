using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }
    }
}