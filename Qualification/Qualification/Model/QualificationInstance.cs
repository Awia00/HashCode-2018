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
        public List<Ride> Rides { get;  }
        
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}