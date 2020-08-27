using Calculator2.Calculator.Data;
using Calculator2.Calculator.Data.Operator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator2.Calculator
{
    public static class ReversePolishNotationHendler
    {
        public static object[] Hendler(object[] InData, Setting setting)
        {
            Stack<object> resulStack = new Stack<object>();
            Stack<object> BuferOperator = new Stack<object>();
            foreach (var elem in InData)
            {
                if (elem.GetType() != typeof(StandartOperator) && elem.GetType() != typeof(char))
                    resulStack.Push(elem);
                else
                {
                    StandartOperator @operator = elem as StandartOperator;
                    if (BuferOperator.Count > 0 && !CheckChar(elem, setting.OperatorParenthesisIn))
                    {
                        if (CheckChar(elem, setting.OperatorParenthesisOut))
                        {
                            while (BuferOperator.Count > 0 && !CheckChar(BuferOperator.Peek(), setting.OperatorParenthesisIn))
                                resulStack.Push(BuferOperator.Pop());
                            BuferOperator.Pop();
                            continue;
                        }
                        else if (@operator.Priority > GetPriority(BuferOperator.Peek()))
                            BuferOperator.Push(@operator);
                        else
                        {
                            while (BuferOperator.Count > 0 && @operator.Priority <= GetPriority(BuferOperator.Peek()))
                                resulStack.Push(BuferOperator.Pop());
                            BuferOperator.Push(@operator);
                        }
                    }
                    else
                        BuferOperator.Push(elem);
                    bool CheckChar(object e, char Check) => e is char C && C == Check;
                    int GetPriority(object e)
                    {
                        if (e is StandartOperator @operator)
                            return @operator.Priority;
                        else
                            return -1;
                    }
                }
            }

            foreach (var elem in BuferOperator)
                resulStack.Push(elem);

            return resulStack.ToArray().Reverse().ToArray();
        }
    }
}
