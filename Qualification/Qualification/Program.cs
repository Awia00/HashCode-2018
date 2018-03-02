using System;
using System.Threading;
using Windemann.HashCode.Qualification.Heuristics;

namespace Windemann.HashCode.Qualification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                tokenSource.Cancel();
            };
            
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage (Windows): Qualification.exe <filename>");
                Console.Error.WriteLine("Usage (Ubuntu): dotnet Qualification.dll <filename>");
                return;
            }
            
            var parser = new QualificationInstanceParser();

            var instance = parser.ParseInstance(args[0]);
            
            Console.Error.WriteLine("Instance has been parsed.");

            var solver = new QualificationSolverBandB(instance, tokenSource.Token);

            var result = solver.Solve();

            Console.Error.WriteLine("Result has been computed.");

            Console.WriteLine(result);
        }
    }
}