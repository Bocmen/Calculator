using System;

namespace Calculator.Data
{
    public static class Const
    {
        /// <summary>
        /// Строчное обозначение логарифма OperatorLog(x,y)
        /// </summary>
        public const string OperatorLog = "log";

        /// <summary>
        /// Символьные обозначения операторов
        /// </summary>
        public const char
            OperatorParenthesisIn = '(', // Индекс 0
            OperatorParenthesisOut = ')', // Индекс 1
            OperatorLogChar = 'l', // Индекс 2
            OperatorPlus = '+', // Индекс 3
            OperatorMinus = '-', // Индекс 4
            OperatorMultiply = '*', // Индекс 5
            OperatorShare = '/', // Индекс 6
            OperatorPow = '^'; // Индекс 7

        /// <summary>
        /// Список операций с двумя числами (если какого то оператора нет в списке Calculate выдаст исключение)
        /// </summary>
        public static Operator[] Operators { get; } = new Operator[]
        {
            new Operator (OperatorParenthesisIn, 0),
            new Operator (OperatorParenthesisOut, 0),
            new Operator (OperatorPlus, 1),
            new Operator (OperatorMinus, 1),
            new Operator (OperatorShare, 2),
            new Operator (OperatorMultiply, 2),
            new Operator (OperatorPow, 3),
            new Operator (OperatorLogChar, 4),
        };
        /// <summary>
        /// Применяет к двум элементам оператор
        /// </summary>
        /// <param name="Two">Второй элемент</param>
        /// <param name="One">Первый элемент</param>
        /// <param name="operator">Применяемый оператор</param>
        public static double CalculateTwoElem(double Two, double One, Operator @operator)
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
    }
}
