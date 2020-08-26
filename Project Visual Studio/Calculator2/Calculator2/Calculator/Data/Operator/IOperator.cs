using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator2.Calculator.Data.Operator
{
    interface IOperator
    {
        double Calculate(params object[] vs);
        object Clone();
    }
}
