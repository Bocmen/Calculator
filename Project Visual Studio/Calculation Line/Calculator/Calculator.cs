using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public static class Calculator
    {
        /// <summary>
        /// Строчное обозначение логарифма OperatorLog(x,y)
        /// </summary>
        private const string OperatorLog = "log";

        /// <summary>
        /// Символьные обозначения операторов
        /// </summary>
        private const char
            OperatorParenthesisIn = '(',
            OperatorParenthesisOut = ')',
            OperatorLogChar = 'l',
            OperatorPlus = '+',
            OperatorMinus = '-',
            OperatorMultiply = '*',
            OperatorShare = '/',
            OperatorPow = '^';


        /// <summary>
        /// Список операций с двумя числами (если какого то оператора нет в списке Calculate выдаст исключение)
        /// </summary>
        private static Operator[] Operators = new Operator[]
        {
            new Operator { Symbol = OperatorParenthesisIn, Priority = 0 },
            new Operator { Symbol = OperatorParenthesisOut, Priority = 0 },
            new Operator { Symbol = OperatorPlus, Priority = 1 },
            new Operator { Symbol = OperatorMinus, Priority = 1 },
            new Operator { Symbol = OperatorShare, Priority = 2 },
            new Operator { Symbol = OperatorMultiply, Priority = 2 },
            new Operator { Symbol = OperatorPow, Priority = 3 },
            new Operator { Symbol = OperatorLogChar, Priority = 4 }, // Логарифм
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
        private static string FilterStr(string str) => FilterLog(str.Replace('.', ','));
        /// <summary>
        /// Делает выражение log(X,Y) читабельным для алгоритма
        /// </summary>
        private static string FilterLog(string str)
        {
            MatchCollection matches = new Regex(OperatorLog).Matches(str);
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                // Индекс начала Log
                int StartIndex = matches[i].Index + OperatorLog.Length + 1;
                // Индекс Окончания выражения Log(X,Y)
                int EndIndex = SearchParenthesis(StartIndex - 1);
                // Получаем параметры X, Y
                string[] param = str.Substring(StartIndex, EndIndex - StartIndex).Split(',');
                // Вставляем исправленое выражение дя подсчёта олгоритмом YlX
                str = $"{str.Substring(0, matches[i].Index)}{OperatorParenthesisIn}{Calculate(param[1])}{OperatorLogChar}{Calculate(param[0])}{OperatorParenthesisOut}{str.Substring(EndIndex + 1, str.Length - EndIndex - 1)}";
                // Поиск индекса закрывающей скобки
                int SearchParenthesis(int StartSearchIndex)
                {
                    int CountParenthesis = 0;
                    do
                    {
                        if (str[StartSearchIndex] == OperatorParenthesisIn)
                            CountParenthesis++;
                        else if (str[StartSearchIndex] == OperatorParenthesisOut)
                            CountParenthesis--;
                        StartSearchIndex++;
                    } while (CountParenthesis != 0);
                    return --StartSearchIndex;
                }
            }
            return str;
        }
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
                case OperatorPlus:
                    return One + Two;
                case OperatorMinus:
                    return One - Two;
                case OperatorMultiply:
                    return One * Two;
                case OperatorShare:
                    return One / Two;
                case OperatorPow:
                    return Math.Pow(One, Two);
                case OperatorLogChar:
                    return Math.Log(One, Two);
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
                if (list.Count > 0 && list.Last().GetType() == typeof(Operator) && ((Operator)list.Last()).Priority != 0)
                {
                    Operator o = Operators.FirstOrDefault(x => x.Symbol == str[i]);
                    if (o != null && o.Priority == 1)
                    {
                        rS += o.Symbol;
                        i++;
                    }
                }
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
        /// Данные об операторе (для взаимодействия с двумя числами)
        /// </summary>
        public class Operator
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
                return string.Format("Символ: {0} Приоритет: {1}", Symbol, Priority);
            }
        }
    }
}