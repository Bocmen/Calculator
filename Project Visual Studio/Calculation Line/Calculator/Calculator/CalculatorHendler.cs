using System;
using System.Collections.Generic;
using System.Text;
using static Calculator.Data.Const;
using Calculator.Data;

namespace Calculator.Calculator
{
    public static class CalculatorHendler
    {
        public static double Calculate(string str)
        {
            try
            {
                object[] ElemsReversePolishNotationHendler = ReversePolishNotationHendler.Hendler(str);
                Stack<double> Bufer = new Stack<double>();
                foreach (var elem in ElemsReversePolishNotationHendler)
                {
                    if (elem.GetType() == typeof(double))
                        Bufer.Push((double)elem);
                    else
                        Bufer.Push(CalculateTwoElem(Bufer.Pop(), Bufer.Pop(), (Operator)elem));
                }
                return Bufer.Pop();
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(Data.Exception.OperatorExeption)) 
                    throw new Data.Exception.ExpressionExeption();
                throw e;
            }
        }
    }
}
