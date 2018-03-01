using System;
using System.IO;

namespace Qualification
{
    public abstract class InstanceParser<TInstance>
    {
        public abstract TInstance ParseInstance(TextReader reader);

        public virtual TInstance ParseInstance(string filename)
        {
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