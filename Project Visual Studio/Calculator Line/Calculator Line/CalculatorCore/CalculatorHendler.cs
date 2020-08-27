using CalculatorCore.Data;
using CalculatorCore.Data.Operator;
using System.Collections.Generic;
using static CalculatorCore.StringParser;

namespace CalculatorCore
{
    public static class CalculatorHendler
    {
        public static double Calculate(string str, Setting setting) => Calculate(SearchOperators(str.ToLower().Replace(" ",""), setting), setting);
        public static double Calculate(object[] vs, Setting setting)
        {
            vs = ReversePolishNotationHendler.Hendler(vs, setting);
            Stack<double> Bufer = new Stack<double>();
            foreach (var elem in vs)
            {
                if (elem.GetType() != typeof(StandartOperator))
                    Bufer.Push(CalculeteOperatorNotStandartOperator(elem, setting));
                else
                    Bufer.Push(((IOperator)elem).Calculate(setting, Bufer.Pop(), Bufer.Pop()));
            }
            return Bufer.Pop();
        }
        private static double CalculeteOperatorNotStandartOperator(object e, Setting setting)
        {
            if (e is char C) return C;
            if (e is double D) return D;
            AddDataOperator dataOperator = (AddDataOperator)e;
            if (dataOperator.Operator is EndValueOperator && dataOperator.Data.GetType() != typeof(double))
            {
                if (dataOperator.Data.GetType() == typeof(object[]))
                    return ((IOperator)dataOperator.Operator).Calculate(setting, Calculate((object[])dataOperator.Data, setting));
                return ((IOperator)dataOperator.Operator).Calculate(setting, CalculeteOperatorNotStandartOperator(dataOperator.Data, setting));
            }
            return ((IOperator)dataOperator.Operator).Calculate(setting, dataOperator.Data);
        }
    }
}
