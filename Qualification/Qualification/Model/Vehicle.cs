using System;

namespace Windemann.HashCode.Qualification.Model
{
    public class Vehicle
    {
        private static int _id;

        public int Id { get; }
        public Coordinate Position { get; set; }
        public int TimeAvailable { get; set; } // Tells when the car is available from Position.

        public Vehicle()
        {
            Id = _id++;
            Position = new Coordinate();
        }

        public Vehicle(int id, Coordinate position, int timeAvailable)
        {
            Id = id;
            Position = position;
            TimeAvailable = timeAvailable;
        }

        protected bool Equals(Vehicle other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vehicle) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public int PossiblePickupTime(Ride ride)
        {
            return Math.Max(TimeAvailable + Position.DistanceTo(ride.Start), ride.EarliestStart);
        }

        public bool CanPickup(Ride ride, int numberOfSteps)
        {
            return PossiblePickupTime(ride) < Math.Min(ride.LatestFinish, numberOfSteps);
        }
    }
}
