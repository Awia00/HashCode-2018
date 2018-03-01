using System;
using System.Collections.Generic;
using System.Text;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public interface IQualificationSolver
    {
        QualificationResult Solve();
    }
}
