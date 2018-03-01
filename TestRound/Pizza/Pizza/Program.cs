using System;
using System.Collections.Generic;

namespace Pizza
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new PizzaParser();

            var instance = parser.ParseInstance("/home/mikael/Hentet/small.in");
            
            
            
            

            Console.WriteLine(instance);
        }

        private IEnumerable<PizzaSlice> GenerateAllSlices(PizzaInstance instance)
        {
            
        }
    }
}