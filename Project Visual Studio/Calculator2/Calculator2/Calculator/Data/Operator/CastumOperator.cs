using System;

namespace Calculator2.Calculator.Data.Operator
{
    public class CastumOperator : IOperator, IName
    {
        public delegate double MetotCalculate(string str);
        public delegate int GetEndIndex(int StartIndexSearch, string str);

        public readonly string Designation;
        private readonly GetEndIndex getEndIndex;
        private readonly MetotCalculate CalculateMetod;
        public object Data;

        public CastumOperator(string Designation, MetotCalculate CalculateMetod, GetEndIndex getEndIndex)
        {
            this.Designation = Designation.ToLower();
            this.CalculateMetod = CalculateMetod;
            this.getEndIndex = getEndIndex;
        }

        public int GetEndIndexSearch(int StartIndex, string str) => getEndIndex.Invoke(StartIndex + Designation.Length, str);

        public MetotCalculate GetMetotCalculate() => CalculateMetod;

        double IOperator.Calculate(params object[] vs)
        {
            if (vs.Length == 1 && vs[0].GetType() == typeof(string))
                return CalculateMetod((string)vs[0]);
            throw new Exception("Ошибка во входных параметрах оператора");
        }

        public override string ToString() => $"{Designation}";

        string IName.GetName() => Designation;
    }
}
