using System.Collections.Generic;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public class BbNode
    {
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public Dictionary<int,HashSet<int>> Conflicts { get; set; } = new Dictionary<int, HashSet<int>>();
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public SortedSet<Ride> Rides { get; set; } = new SortedSet<Ride>(new RideComparer());
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }

        public BbNode(int numVehicles)
        {
            for (int i = 0; i < numVehicles; i++)
            {
                Conflicts.Add(i, new HashSet<int>);
            }
        }

        public BbNode(BbNode node)
        {
            Assignments = new List<Assignment>(node.Assignments);
            Conflicts = new Dictionary<int, HashSet<int>>(node.Conflicts);
            Vehicles = new List<Vehicle>(node.Vehicles);
            Rides = new SortedSet<Ride>(node.Rides);
        }
    }
}