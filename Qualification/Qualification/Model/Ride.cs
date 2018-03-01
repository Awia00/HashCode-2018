using System;
using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification.Model
{
    public class Ride
    {
        public int Id { get; }
        public int StartRow { get; }
        public int StartColumn { get; }
        public int FinishRow { get; }
        public int FinishColumn { get; }
        public int EarliestStart { get; }
        public int LatestFinish { get; }

        public Ride(int id, int startRow, int startColumn, int finishRow, int finishColumn, int earliestStart, int latestFinish)
        {
            Id = id;
            StartRow = startRow;
            StartColumn = startColumn;
            FinishRow = finishRow;
            FinishColumn = finishColumn;
            EarliestStart = earliestStart;
            LatestFinish = latestFinish;
        }
    }
}
