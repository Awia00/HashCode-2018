using System.Collections.Generic;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    class VehicleTimeComparer : IComparer<Vehicle>
    {
        public int Compare(Vehicle x, Vehicle y)
        {
            return x.TimeAvailable <= y.TimeAvailable ? -1 : 1;
        }
    }
}