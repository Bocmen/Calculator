using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public static class Calculator
    {
        private static Operator[] Operators = new Operator[]
        {
            new Operator { Symbol = ')', Priority = 0 },
            new Operator { Symbol = '(', Priority = 0 },
            new Operator { Symbol = '+', Priority = 1 },
            new Operator { Symbol = '-', Priority = 1 },
            new Operator { Symbol = '/', Priority = 2 },
            new Operator { Symbol = '*', Priority = 2 },
            new Operator { Symbol = '^', Priority = 3 }
        };
        public static double Calculate(string str)
        {
            try
            {
                object[] Elements = HendlerObject(str.Replace('.', ',').Replace("+-", "-").Replace("-+", "-"));
                Stack<double> Number = new Stack<double>();
                Stack<Operator> operators = new Stack<Operator>();
                foreach (var elem in Elements)
                {
                    // Число ли это
                    if (elem.GetType() == typeof(double))
                    {
                        Number.Push((double)elem);
                    }
                    else
                    {
                        // Если предыдущий оператор по приоритету меньше или равен
                        if (operators.Count > 0 && Number.Count > 1 && operators.Peek().Priority != 0 && ((Operator)elem).Priority != 0 && operators.Peek().Priority >= ((Operator)elem).Priority)
                        {
                            Number.Push(CalculateTwoElem(Number.Pop(), Number.Pop(), operators.Pop()));
                        }
                        if (((Operator)elem).Priority == 0 && ((Operator)elem).Symbol == ')')
                        {
                            while (operators.Peek().Symbol != '(')
                                Number.Push(CalculateTwoElem(Number.Pop(), Number.Pop(), operators.Pop()));
                            operators.Pop();
                        }
                        else
                            operators.Push((Operator)elem);
                    }
                }
                for (int i = operators.Count; i > 0; i--)
                    Number.Push(CalculateTwoElem(Number.Pop(), Number.Pop(), operators.Pop()));
                return Number.Pop();
            }
            catch
            {
                throw new Exception("Введенное выражение неправильно записано");
            }
        }
        private static double CalculateTwoElem(double Two, double One, Operator @operator)
        {
            switch (@operator.Symbol)
            {
                case '+':
                    return One + Two;
                case '-':
                    return One - Two;
                case '*':
                    return One * Two;
                case '/':
                    return One / Two;
                case '^':
                    return Math.Pow(One, Two);
                default:
                    return 0;
            }
        }
        private static object[] HendlerObject(string str)
        {
            List<object> list = new List<object>();
            for (int i = 0; i < str.Length; i++)
            {
                string rS = null;
                while (i < str.Length && (str[i] == ',' || double.TryParse(str[i].ToString(), out double r)))
                    rS += str[i++];
                if (rS != null)
                {
                    i--;
                    list.Add(double.Parse(rS));
                }
                else
                    list.Add(Operators.First(x => x.Symbol == str[i]));
            }
            return list.ToArray();
        }
        public struct Operator
        {
            public char Symbol;
            public byte Priority;
        }
    }
}