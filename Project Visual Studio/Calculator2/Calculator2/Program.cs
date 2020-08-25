using Calculator2.Calculator;
using Calculator2.Calculator.Data;
using System;

namespace Calculator2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Setting setting = new Setting();
            setting.AddOperator(new Calculator.Data.Operator.CastumOperator("Sin", (x) => 0, SearchEndIndex));
            string s = "sin|sin|sin|sin|||||sin||sin||sin|sin||sin||sin|||";
            char[] Arr = s.ToCharArray();
            Console.WriteLine(string.Join("   ", Arr));
            for (int i = 0; i < Arr.Length; i++)
            {
                string S = i.ToString();
                if(S.Length < 2)
                    s += " ";
                Console.Write(S + " ");
            }
            StringParser.SearchOperators(s, setting);
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
    }
}
