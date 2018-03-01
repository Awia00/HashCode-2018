using System.Text;

namespace Pizza
{
    public class PizzaInstance
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int MinimumOfEachIngredient { get; set; }
        public int MaximumCellsInSlice { get; set; }
        public PizzaIngredient[,] Pizza { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{Rows} {Columns} {MinimumOfEachIngredient} {MaximumCellsInSlice}");

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    builder.Append(Pizza[i, j] == PizzaIngredient.Mushroom ? 'M' : 'T');
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}