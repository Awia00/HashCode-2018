﻿using System;
using System.Collections.Generic;
using System.Text;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    interface IQualificationSolver
    {
        QualificationResult Solve(QualificationInstance instance);
    }
}
