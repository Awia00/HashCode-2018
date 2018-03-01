using System.Collections.Generic;
using System.Text;

namespace Pizza
{
    class PizzaResult
    {
        public ICollection<PizzaSlice> Slices { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(Slices.Count.ToString());
            foreach (var slice in Slices)
            {
                builder.AppendLine(slice.ToString());
            }

            return builder.ToString();
        }
    }
}