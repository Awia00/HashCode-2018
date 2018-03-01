using System.Collections.Generic;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public class BbNode
    {
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public Dictionary<int,int> Conflicts { get; set; } = new Dictionary<int, int>();
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public SortedSet<Ride> Rides { get; set; } = new SortedSet<Ride>(new RideComparer());
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
    }
}