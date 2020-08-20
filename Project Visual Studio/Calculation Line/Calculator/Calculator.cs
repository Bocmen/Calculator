using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public static class Calculator
    {
        /// <summary>
        /// Список операций (если какого то оператора нет в списке Calculate выдаст исключение)
        /// </summary>
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
        /// <summary>
        /// Подсчёт выражения из строки
        /// </summary>
        /// <returns>Результат выражения</returns>
        public static double Calculate(string str)
        {
            try
            {
                object[] Elements = HendlerObject(FilterStr(str));
                Stack<double> Number = new Stack<double>();
                Stack<Operator> operators = new Stack<Operator>();
                foreach (var elem in Elements)
                {
                    if (elem.GetType() == typeof(double))
                    {
                        Number.Push((double)elem);
                    }
                    else
                    {
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
        /// <summary>
        /// Удаляет ненужные части (подготавливает строку к разбитию её на числа и операторы)
        /// </summary>
        private static string FilterStr(string str) => str.Replace('.', ',').Replace("+-", "-").Replace("-+", "-");
        /// <summary>
        /// Применяет к двум элементам оператор
        /// </summary>
        /// <param name="Two">Второй элемент</param>
        /// <param name="One">Первый элемент</param>
        /// <param name="operator">Применяемый оператор</param>
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
        /// <summary>
        /// Разбитие строки на числа и операторы
        /// </summary>
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
        /// <summary>
        /// Данные об операторе
        /// </summary>
        public struct Operator
        {
            /// <summary>
            /// Его обозначение в виде символа
            /// </summary>
            public char Symbol;
            /// <summary>
            /// Его приоритет
            /// </summary>
            public byte Priority;
            public override string ToString()
            {
                return string.Format("Символ: {0}, Приоритет: {1}", Symbol, Priority);
            }
        }
    }
}