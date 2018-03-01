using System;
using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification.Model
{
    public class Vehicle
    {
        public Coordinate Position { get; set; }
        public int TimeAvailable { get; set; } // Tells when the car is available from Position.

        public Vehicle()
        {
            Position = new Coordinate();
        }

        public Vehicle(Coordinate position, int timeAvailable)
        {
            Position = position;
            TimeAvailable = timeAvailable;
        }
    }
}
