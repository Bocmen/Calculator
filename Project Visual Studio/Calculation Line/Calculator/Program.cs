using Calculator.Calculator;
using System;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            Console.Write("Напишите выражение: ");
            try
            {
                Console.WriteLine("\r\n{0}\r\n", CalculatorHendler.Calculate(Console.ReadLine()));
            }
            catch (Exception e)
            {
                Console.WriteLine("\r\n{0}\r\n", e.Message);
            }
            Main();
        }
    }
}
