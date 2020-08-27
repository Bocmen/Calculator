using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator2.Calculator.Data.Operator
{
    interface IOperator
    {
        double Calculate(Setting setting, params object[] vs);
    }
}
