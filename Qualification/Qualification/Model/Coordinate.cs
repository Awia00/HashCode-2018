using System;
using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification.Model
{
    public class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public int DistanceTo(Coordinate other) => Math.Abs(Row - other.Row) + Math.Abs(Column - other.Column);
    }
}
