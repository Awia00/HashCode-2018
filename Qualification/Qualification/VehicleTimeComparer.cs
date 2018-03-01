using System.Collections.Generic;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    class VehicleTimeComparer : IComparer<Vehicle>
    {
        public int Compare(Vehicle x, Vehicle y)
        {
            if (x.TimeAvailable < y.TimeAvailable)
                return -1;
            return x.TimeAvailable == y.TimeAvailable ? 0 : 1;
        }
    }
}