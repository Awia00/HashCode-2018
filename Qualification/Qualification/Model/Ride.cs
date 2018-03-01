using System;
using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification.Model
{
    public class Ride
    {
        public int Id { get; }
        public Coordinate Start { get; }
        public Coordinate End { get; }
        public int EarliestStart { get; }
        public int LatestFinish { get; }
        public int Distance { get; }

        public Ride(int id, int startRow, int startColumn, int finishRow, int finishColumn, int earliestStart, int latestFinish)
        {
            Id = id;
            Start = new Coordinate { Row = startRow, Column = startColumn};
            End = new Coordinate { Row = finishRow, Column = finishColumn};
            EarliestStart = earliestStart;
            LatestFinish = latestFinish;
            Distance = Start.DistanceTo(End);
        }

        public int Score(QualificationInstance instance, int pickupTime)
        {
            var bonus = pickupTime <= EarliestStart ? instance.PerRideBonus : 0;

            return bonus + Distance;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Ride;

            return Id == other?.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
