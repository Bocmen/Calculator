using System;

namespace Calculator2.Calculator.Data.Operator
{
    public class StandartOperator : IOperator, IName
    {
        public delegate double MetotCalculate(double One, double TWo);

        public readonly char Symbol;
        public readonly byte Priority;
        private readonly MetotCalculate CalculateMetod;

        public StandartOperator(char Symbol, byte Priority, MetotCalculate CalculateMetod)
        {
            this.Symbol = Symbol.ToString().ToLower()[0];
            this.Priority = Priority;
            this.CalculateMetod = CalculateMetod;
        }

        public MetotCalculate GetMetotCalculate() => CalculateMetod;


        double IOperator.Calculate(Setting setting ,params object[] vs)
        {
            if (vs.Length == 2)
            {
                foreach (var elem in vs)
                    if (elem.GetType() != typeof(double))
                        throw new Exception("Неверные параметры для оператора");
                return CalculateMetod.Invoke((double)vs[1], (double)vs[0]);
            }
            throw new ArgumentException("Неверное кол-во входных операторов у оператора");
        }
        public override string ToString() => $"{Symbol} приоритет: {Priority}";

        string IName.GetName() => Symbol.ToString();
    }
}
