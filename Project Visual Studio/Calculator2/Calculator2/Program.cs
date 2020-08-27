using Calculator2.Calculator;
using Calculator2.Calculator.Data;
using Calculator2.Calculator.Data.Operator;
using System;
using System.Resources;

namespace Calculator2
{
    class Program
    {
        static void Main(string[] args)
        {
            Setting setting = new Setting();
            setting.AddOperator(new CastumOperator("Sin", CalculateSin, SearchEndIndex, true));
            setting.AddOperator(new EndValueOperator("!", Factorial));
        restart:
            try
            {
                Console.Write("Напишите выражение: ");
                Console.WriteLine($"\r\n{CalculatorHendler.Calculate(Console.ReadLine(), setting)}\r\n");
            }
            catch
            {
                Console.WriteLine($"\r\nError\r\n");
                goto restart;
            }
            goto restart;
        }
        private static double CalculateSin(string str, Setting setting) => Math.Sin(CalculatorHendler.Calculate(StringParser.SearchOperators(str.Substring(1, str.Length - 2), setting), setting));
        private static double Factorial(double N)
        {
            N = (int)N;
            long factotial = 1;
            if (N != 0)
            {
                for (int i = 2; i <= N; i++)
                {
                    factotial *= i;
                }
            }
            return factotial;
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
