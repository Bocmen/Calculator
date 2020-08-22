using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Calculator.Data
{
    public static class Exception
    {
        public class OperatorExeption : ArgumentException
        {
            public OperatorExeption() : base("Неизвестные символы присутствуют в выражении")
            {
            }
        }
        public class ExpressionExeption : ArgumentException
        {
            public ExpressionExeption() : base("Выражение неверно записано")
            {
            }
        }
    }
}
