using System.Collections.Generic;

namespace Windemann.HashCode.Qualification
{
    class BbNodeComparer : IComparer<BbNode>
    {
        public int Compare(BbNode x, BbNode y)
        {
            if (x.UpperBound < y.UpperBound)
                return -1;
            if (x.UpperBound > y.UpperBound)
                return 1;
            return x.GetHashCode() - y.GetHashCode();
        }
    }
}