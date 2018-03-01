using System;
using System.IO;

namespace Windemann.HashCode.Qualification
{
    public abstract class InstanceParser<TInstance>
    {
        public abstract TInstance ParseInstance(TextReader reader);

        public virtual TInstance ParseInstance(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new ArgumentException($"File '{filename}' not found.");
            }
            
            using (var file = File.OpenRead(filename))
            {
                using (var reader = new StreamReader(file))
                {
                    return ParseInstance(reader);
                }
            }            
        }

        public virtual TInstance ParseInstance()
        {
            return ParseInstance(Console.In);
        }
    }
}