using Calculator.Calculator;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Напишите выражение: ");
            string str = "((5+5)+6+(5+5)+7)";
            Console.WriteLine();
            var r = CalculateStr(str);

            Console.ReadKey();
        }

        private static double CalculateStr(string str)
        {
            Parenthesis.ParenthesisData[] parenthesisDatas = Parenthesis.GetData(str);
            int[] CountChildrenSort = parenthesisDatas.Select(x => x.CountChildren).GroupBy(x => x).Select(x => x.First()).ToImmutableSortedSet().ToArray();
            for (int i = 0; i < CountChildrenSort.Length; i++)
            {
                int[] vs = parenthesisDatas.Select((x, y) => (x, y)).Where(x => x.x.CountChildren == CountChildrenSort[i]).Select(x => x.y).ToArray();
                for (int j = 0; j < vs.Length; j++)
                {
                    double r = GetResul(str.Substring(parenthesisDatas[i].IndexIn + 1, parenthesisDatas[i].IndexOut - parenthesisDatas[i].IndexIn - 1));
                }
            }
            return 0;
        }
        private static double GetResul(string str)
        {
            return 0;
        }

    }
}
