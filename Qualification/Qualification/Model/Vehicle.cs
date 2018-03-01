using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
