using Calculator2.Calculator;
using Calculator2.Calculator.Data;
using Calculator2.Calculator.Data.Operator;
using System;
using static Calculator2.Calculator.Data.StringParser;

namespace Calculator2
{
    class Program
    {
        static void Main(string[] args)
        {
            Setting setting = new Setting();
            setting.AddOperator(new CastumOperator("Sin", (x) => 0, SearchEndIndex, true));
            setting.AddOperator(new EndValueOperator("!", (x) => x));
            string s = "5!!";// "sin|34|+sin|0|"; // "5!+6!";// "-(56+36)!+(56)!";
            char[] Arr = s.ToCharArray();
            var r = StringParser.SearchOperators(s, setting);
            Console.ReadLine();
        }
        private static int SearchEndIndex(int start, string str)
        {
            byte b = 0;
            for (int i = start; i < str.Length; i++)
            {
                if (str[i] == '|') b++;
                if (b == 2) return i;
            }
            return 0;
        }
        //TOODO знак , добавить ему проверку на совпадение с операторами
    }
}
