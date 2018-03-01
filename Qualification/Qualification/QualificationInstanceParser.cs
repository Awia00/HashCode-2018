using System;
using System.Collections.Generic;
using System.IO;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public sealed class QualificationInstanceParser : InstanceParser<QualificationInstance>
    {
        public override QualificationInstance ParseInstance(TextReader reader)
        {
            var line = reader.ReadLine();
            
            if (string.IsNullOrWhiteSpace(line)) throw new FormatException();

            var tokens = line.Split(' ');

            var r = int.Parse(tokens[0]);
            var c = int.Parse(tokens[1]);
            var f = int.Parse(tokens[2]);
            var n = int.Parse(tokens[3]);
            var b = int.Parse(tokens[4]);
            var t = int.Parse(tokens[5]);

            var rides = new List<Ride>();

            for (var i = 0; i < n; ++i)
            {
                line = reader.ReadLine();
                
                if (string.IsNullOrWhiteSpace(line)) throw new FormatException();

                tokens = line.Split(' ');
                
                var aRide = int.Parse(tokens[0]);
                var bRide = int.Parse(tokens[1]);
                var xRide = int.Parse(tokens[2]);
                var yRide = int.Parse(tokens[3]);
                var sRide = int.Parse(tokens[4]);
                var fRide = int.Parse(tokens[5]);
                
                rides.Add(new Ride(i, aRide, bRide, xRide, yRide, sRide, fRide));
            }

            return new QualificationInstance(r, c, f, n, b, t, rides);
        }
    }
}