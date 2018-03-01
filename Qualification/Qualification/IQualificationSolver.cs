using System;
using System.Collections.Generic;
using System.Text;

namespace Windemann.HashCode.Qualification
{
    interface IQualificationSolver
    {
        QualificationResult Solve(QualificationInstance instance);
    }
}
