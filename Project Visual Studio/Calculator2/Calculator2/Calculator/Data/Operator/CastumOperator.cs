﻿using System;

namespace Calculator2.Calculator.Data.Operator
{
    public class CastumOperator : IOperator, IName
    {
        public delegate double MetotCalculate(string str);
        public delegate int GetEndIndex(int StartIndexSearch, string str);

        public readonly string Designation;
        private readonly GetEndIndex getEndIndex;
        private readonly MetotCalculate CalculateMetod;
        public readonly bool HelperSearchIndex;
        public object Data;

        public CastumOperator(string Designation, MetotCalculate CalculateMetod, GetEndIndex getEndIndex, bool HelperSearchIndex = true)
        {
            this.Designation = Designation.ToLower();
            this.CalculateMetod = CalculateMetod;
            this.getEndIndex = getEndIndex;
            this.HelperSearchIndex = HelperSearchIndex;
        }

        public int GetEndIndexSearch(int StartIndex, string str) => getEndIndex.Invoke(StartIndex + Designation.Length, str);

        public MetotCalculate GetMetotCalculate() => CalculateMetod;

        double IOperator.Calculate(params object[] vs)
        {
            if (vs.Length == 1 && vs[0].GetType() == typeof(string))
                return CalculateMetod((string)vs[0]);
            throw new ArgumentException("Ошибка во входных параметрах оператора");
        }

        public override string ToString() => $"{Designation}";

        string IName.GetName() => Designation;

        public object Clone() => new CastumOperator(Designation, CalculateMetod, getEndIndex, HelperSearchIndex);
    }
}
