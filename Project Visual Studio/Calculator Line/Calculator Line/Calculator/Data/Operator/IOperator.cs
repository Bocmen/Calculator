using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCore.Data.Operator
{
    interface IOperator
    {
        double Calculate(Setting setting, params object[] vs);
    }
}
