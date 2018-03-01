using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace Pizza
{
    class Program
    {
        internal static void Main(string[] args)
        {
            var parser = new PizzaParser();

            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: dotnet Pizza.dll <filename>");
                return;
            }

            var instance = parser.ParseInstance(args[0]);

            Console.Error.WriteLine("Generating all slices");
            
            var slices = GenerateAllSlices(instance).ToList();
            
            Console.Error.WriteLine($"{slices.Count} slices generated");
            Console.Error.WriteLine("Ordering all slices by size");
                
            var orderedSlices = slices.AsParallel().OrderByDescending(s => s.Size);
            
            Console.Error.WriteLine("Doing greedy heuristic");

            var solution = new List<PizzaSlice>();

            var bitmap = new bool[instance.Rows, instance.Columns];

            var count = 0;
            foreach (var slice in orderedSlices)
            {
                count++;
                var isGood = true;
                for (var row = slice.TopRow; row <= slice.BottomRow; ++row)
                {
                    for (var col = slice.LeftColumn; col <= slice.RightColumn; ++col)
                    {
                        if (bitmap[row, col])
                        {
                            isGood = false;
                            break;
                        }
                    }

                    if (!isGood) break;
                }

                if (isGood)
                {
                    for (var row = slice.TopRow; row <= slice.BottomRow; ++row)
                    {
                        for (var col = slice.LeftColumn; col <= slice.RightColumn; ++col)
                        {
                            bitmap[row, col] = true;
                        }
                    }

                    solution.Add(slice);
                    Console.Error.WriteLine($"Number of slices taken: {solution.Count} - Slices considered: {count}");
                }
            }

            Console.WriteLine(new PizzaResult
            {
                Slices = solution
            });
            
            Console.Error.WriteLine(solution.Count);
        }

        private static IEnumerable<PizzaSlice> GenerateAllSlices(PizzaInstance instance)
        {
            for (var startRow = 0; startRow < instance.Rows; startRow++)
            {
                for (var startCol = 0; startCol < instance.Columns; startCol++)
                {
                    for (var rows = 1; startRow + rows < instance.Rows && rows < instance.MaximumCellsInSlice; rows++)
                    {
                        for (var cols = Math.Max(1, instance.MinimumOfEachIngredient / rows); startCol + cols < instance.Columns && rows * cols < instance.MaximumCellsInSlice; cols++)
                        {
                            var slice = new PizzaSlice(startRow, startCol, startRow + rows - 1, startCol + cols - 1);

                            if (slice.IsValid(instance))
                            {
                                yield return slice;
                            }                            
                        }                        
                    }
                }                
            }
        }
    }
}