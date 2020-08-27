using CalculatorCore.Data;
using CalculatorCore.Data.Operator;
using System;
using System.Collections.Generic;
using System.Text;
using static CalculatorCore.CalculatorHendler;

namespace CalculatorCore
{
    public static class CalculatorCastumSetting
    {
        private static Setting setting;
        public static Setting Setting
        {
            get
            {
                if (setting == null)
                    Start();
                return setting;
            }
            private set
            {

            }
        }
        private static void Start()
        {
            setting = new Setting();
            setting.AddOperator(new CustomOperator("Sin", (a, b) => Math.Sin(Calculate(a, b)), SearchEndIndexParenthesis, false));
            setting.AddOperator(new CustomOperator("Cos", (a, b) => Math.Cos(Calculate(a, b)), SearchEndIndexParenthesis, false));
            setting.AddOperator(new CustomOperator("tg", (a, b) => Math.Tan(Calculate(a, b)), SearchEndIndexParenthesis, false));
            setting.AddOperator(new CustomOperator("ctg", (a, b) => 1 / Math.Tan(Calculate(a, b)), SearchEndIndexParenthesis, false));
            // setting.AddOperator(new CustomOperator("Abs", (a,b) => Math.Abs(Calculate(a, b)), SearchEndIndexParenthesis, true));
            setting.AddOperator(new CustomOperator("|", CalculateModule, SearchEndIndexOperatorModule, true));
            setting.AddOperator(new EndValueOperator("!", Factorial));
        }
        private static int SearchEndIndexOperatorModule(int start, string str)
        {
            for (int i = start; i < str.Length; i++)
            {
                if (str[i] == '|')
                    return i;
            }
            return start - 2;
        }
        private static double CalculateModule(string a, Setting setting)
        {
            a = a.Substring(0, a.Length - 1);
            return Math.Abs(Calculate(a, setting));
        }

        private static int SearchEndIndexParenthesis(int start, string str)
        {
            int Count = 0;
            for (int i = start; i < str.Length; i++)
            {
                if (str[i] == Setting.OperatorParenthesisIn)
                    Count++;
                if (str[i] == Setting.OperatorParenthesisOut)
                    Count--;
                if (Count == 0) return i;
            }
            return start;
        }
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
    }
}
