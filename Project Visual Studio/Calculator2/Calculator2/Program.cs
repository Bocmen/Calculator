using Calculator2.Calculator;
using Calculator2.Calculator.Data;
using Calculator2.Calculator.Data.Operator;
using System;

namespace Calculator2
{
    class Program
    {
        static void Main(string[] args)
        {
            Setting setting = new Setting();
            setting.AddOperator(new CastumOperator("Sin", CalculateSin, SearchEndIndex, true));
            setting.AddOperator(new EndValueOperator("!", (x, y) => Math.Pow(x, 2)));
            string s = "sin|sin|360||"; // "(4+2*-1)!!!";//"(4-2)!!!";// "5+-sin|5|+sin|636--5|!!--(1)";// "sin|34|+sin|0|"; // "5!+6!";// "-(56+36)!+(56)!";
            var r = CalculatorHendler.Calculate(StringParser.SearchOperators(s, setting), setting);
            Console.ReadLine();
        }
        private static double CalculateSin(string str, Setting setting) => Math.Sin(CalculatorHendler.Calculate(StringParser.SearchOperators(str.Substring(1, str.Length - 2), setting), setting));
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
