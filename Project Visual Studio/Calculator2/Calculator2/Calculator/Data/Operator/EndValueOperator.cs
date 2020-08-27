using System;

namespace Calculator2.Calculator.Data.Operator
{
    public class EndValueOperator : IOperator, IName
    {
        public delegate double MetotCalculate(double d);
        public readonly string Designation;
        private readonly MetotCalculate CalculateMetod;

        public EndValueOperator(string Designation, MetotCalculate CalculateMetod)
        {
            this.Designation = Designation;
            this.CalculateMetod = CalculateMetod;
        }

        double IOperator.Calculate(Setting setting, params object[] vs)
        {
            if (vs.Length == 1)
            {
                if (vs[0].GetType() != typeof(double))
                    throw new Exception("Неверные параметры для оператора");
                return CalculateMetod.Invoke((double)vs[0]);
            }
            throw new ArgumentException("Неверное кол-во входных операторов у оператора");
        }

        string IName.GetName() => Designation;
    }
}
