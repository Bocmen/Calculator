using Calculator.Calculator.Data;
using Calculator.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Calculator.Data.Const;

namespace Calculator.Calculator
{
    public static class StringPars
    {
        /// <summary>
        /// Разбитие строки на числа и операторы
        /// </summary>
        public static object[] HendlerObject(string str)
        {
            try
            {
                str = FilterStr(str);
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
            catch
            {
                throw new Exception.OperatorExeption();
            }
        }
        /// <summary>
        /// Удаляет ненужные части (подготавливает строку к разбитию её на числа и операторы)
        /// </summary>
        private static string FilterStr(string str) => FilterLog(str.Replace('.', ',').Replace(" ", ""));
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
                str = $"{str.Substring(0, matches[i].Index)}{OperatorParenthesisIn}({param[1]}){OperatorLogChar}({param[0]}){OperatorParenthesisOut}{str.Substring(EndIndex + 1, str.Length - EndIndex - 1)}";
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
    }
}
