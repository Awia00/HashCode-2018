using System;
using System.IO;

namespace Pizza
{
    public class PizzaParser
    {
        public PizzaInstance ParseInstance(TextReader reader)
        {
            var line = reader.ReadLine();
            
            if (string.IsNullOrWhiteSpace(line)) throw new FormatException();

            var tokens = line.Split(' ');
            
            var r = int.Parse(tokens[0]);
            var c = int.Parse(tokens[1]);
            var l = int.Parse(tokens[2]);
            var h = int.Parse(tokens[3]);

            var pizza = new PizzaIngredient[r, c];

            for (var i = 0; i < r; i++)
            {
                line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line)) throw new FormatException();
                
                for (var j = 0; j < c; j++)
                {
                    if (line[j] == 'M')
                    {
                        pizza[i, j] = PizzaIngredient.Mushroom;
                    }
                    else
                    {
                        pizza[i, j] = PizzaIngredient.Tomato;
                    }
                }
            }

            return new PizzaInstance
            {
                Rows = r,
                Columns = c,
                MinimumOfEachIngredient = l,
                MaximumCellsInSlice = h,
                Pizza = pizza
            };
        }
        
        public PizzaInstance ParseInstance(string filename)
        {
            using (var file = File.OpenRead(filename))
            {
                using (var reader = new StreamReader(file))
                {
                    return ParseInstance(reader);
                }
            }
        }

        public PizzaInstance ParseInstance()
        {
            return ParseInstance(Console.In);
        }
    }
}