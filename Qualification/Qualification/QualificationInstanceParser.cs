using System;
using System.IO;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public sealed class QualificationInstanceParser : InstanceParser<QualificationInstance>
    {
        public override QualificationInstance ParseInstance(TextReader reader)
        {
            var line = reader.ReadLine();
        }
    }
}