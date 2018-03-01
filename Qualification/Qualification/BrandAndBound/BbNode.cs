using System.Collections.Generic;

namespace Windemann.HashCode.Qualification
{
    class BbNode
    {
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public Dictionary<int,int> Conflicts { get; set; } = new Dictionary<int, int>();
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
    }
}