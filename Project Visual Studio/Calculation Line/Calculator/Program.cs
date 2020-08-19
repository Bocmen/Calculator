using System;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            Console.Write("Напишите выражение: ");
            Console.WriteLine("\r\n{0}\r\n",Calculator.Calculate(Console.ReadLine()));
            Main();
        }
    }
}
