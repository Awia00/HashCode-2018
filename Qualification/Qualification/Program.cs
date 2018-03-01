using System;

namespace Qualification
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: dotnet Qualification.dll <filename>");
                return;
            }
            
            var parser = new QualificationInstanceParser();

            var instance = parser.ParseInstance(args[0]);
            
            Console.Error.WriteLine("Instance has been parsed.");

            var result = FindResult(instance);

            Console.Error.WriteLine("Result has been computed.");

            Console.WriteLine(result);
        }

        static QualificationResult FindResult(QualificationInstance instance)
        {
            throw new NotImplementedException();
        }
    }
}