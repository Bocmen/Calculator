using Calculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using static Calculator.Data.Const;

namespace Calculator.Calculator
{
    public static class ReversePolishNotationHendler
    {
        private static object[] Hendler(object[] InData)
        {
            try
            {
                Stack<object> resulStack = new Stack<object>();
                Stack<Operator> BuferOperator = new Stack<Operator>();
                foreach (var elem in InData)
                {
                    if (elem.GetType() == typeof(double))
                        resulStack.Push(elem);
                    else
                    {
                        Operator e = (Operator)elem;
                        if (BuferOperator.Count > 0 && e.Symbol != OperatorParenthesisIn)
                        {
                            if (e.Symbol == OperatorParenthesisOut)
                            {
                                while (BuferOperator.Count > 0 && BuferOperator.Peek().Symbol != OperatorParenthesisIn)
                                    resulStack.Push(BuferOperator.Pop());
                                BuferOperator.Pop();
                                continue;
                            }
                            else if (e.Priority > BuferOperator.Peek().Priority)
                                BuferOperator.Push(e);
                            else
                            {
                                while (BuferOperator.Count > 0 && e.Priority <= BuferOperator.Peek().Priority)
                                    resulStack.Push(BuferOperator.Pop());
                                BuferOperator.Push(e);
                            }
                        }
                        else
                            BuferOperator.Push(e);
                    }
                }
                foreach (var elem in BuferOperator)
                    resulStack.Push(elem);

                return resulStack.ToArray().Reverse().ToArray();
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(Data.Exception.OperatorExeption))
                    throw new Data.Exception.ExpressionExeption();
                throw e;
            }
        }
        public static object[] Hendler(string str)
        {
            return Hendler(StringPars.HendlerObject(str));
        }
    }
}
