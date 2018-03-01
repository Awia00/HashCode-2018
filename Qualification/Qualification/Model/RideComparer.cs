using System;
using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification.Model
{
    public class RideComparer : IComparer<Ride>
    {
        public int Compare(Ride x, Ride y)
        {
            if (x.EarliestStart < y.EarliestStart)
                return -1;
            if (x.EarliestStart > y.EarliestStart)
                return 1;
            return x.Id - y.Id;
        }
    }
}
