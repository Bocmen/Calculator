using System;
using System.Collections.Generic;
using CalculatorCore.Data.Operator;
using System.Linq;

namespace CalculatorCore.Data
{
    public class Setting
    {
        ///////////////////////////////////////////////////////////////// Обозначение (char) стандартных обязательных операторов и символов
        private char
            operatorParenthesisIn = '(',
            operatorParenthesisOut = ')',
            operatorSeparatorDouble = ',',
            operatorPlus = '+',
            operatorMinus = '-',
            operatorMultiply = '*',
            operatorShare = '/',
            operatorPow = '^';


        /// <summary>
        /// Обозначение разделяющей запятой с числами с плавующей запятой
        /// </summary>
        public char OperatorSeparatorDouble
        {
            get
            {
                return operatorSeparatorDouble;
            }
            set
            {
                if (CheckUnicalName(value.ToString()))
                    operatorSeparatorDouble = value;
            }
        }
        /// <summary>
        /// Обозначение открывающей скобки
        /// </summary>
        public char OperatorParenthesisIn
        {
            get
            {
                return operatorParenthesisIn;
            }
            set
            {
                if (CheckUnicalName(value.ToString()))
                    operatorParenthesisIn = value;
            }
        }
        /// <summary>
        /// Обозначение закрывающей скобки
        /// </summary>
        public char OperatorParenthesisOut
        {
            get
            {
                return operatorParenthesisOut;
            }
            set
            {
                if (CheckUnicalName(value.ToString()))
                    operatorParenthesisOut = value;
            }
        }

        /// <summary>
        /// Оператор сложения и обозначения положительных чисел
        /// </summary>
        public char OperatorPlus
        {
            get
            {
                return operatorPlus;
            }
            set
            {
                UpdateStandartCharOperator(operatorPlus, value);
                operatorPlus = value;
            }
        }
        /// <summary>
        /// Оператор разности и обозачения отрицательных чисел
        /// </summary>
        public char OperatorMinus
        {
            get
            {
                return operatorMinus;
            }
            set
            {
                UpdateStandartCharOperator(operatorMinus, value);
                operatorMinus = value;
            }
        }
        /// <summary>
        /// Оператор умножения
        /// </summary>
        public char OperatorMultiply
        {
            get
            {
                return operatorMultiply;
            }
            set
            {
                UpdateStandartCharOperator(operatorMultiply, value);
                operatorMultiply = value;
            }
        }
        /// <summary>
        /// Оператор деления
        /// </summary>
        public char OperatorShare
        {
            get
            {
                return operatorShare;
            }
            set
            {
                UpdateStandartCharOperator(operatorShare, value);
                operatorShare = value;
            }
        }
        /// <summary>
        /// Оператор возведения в степень
        /// </summary>
        public char OperatorPow
        {
            get
            {
                return operatorPow;
            }
            set
            {
                UpdateStandartCharOperator(operatorPow, value);
                operatorPow = value;
            }
        }
        ///////////////////////////////////////////////////////////////// Данные операторы и константы
        public StandartOperator[] StandartOperators { get; private set; }
        private List<Const> consts = new List<Const>()
        {
            new Const("pi",Math.PI),
            new Const("e",Math.E)
        };
        public Const[] Consts
        {
            get
            {
                return consts.ToArray();
            }
            private set
            {

            }
        }
        private List<object> customOperator = new List<object>();
        public object[] CostumOperator
        {
            get
            {
                return customOperator.ToArray();
            }
            private set
            {

            }
        }
        public object[] GetAllOperator
        {
            get
            {
                List<object> list = new List<object>();
                list.AddRange(StandartOperators);
                list.AddRange(customOperator);
                return list.ToArray();
            }
            private set
            {

            }
        }
        public Setting()
        {
            // Инцилизация стандартных операторов
            StandartOperators = new StandartOperator[]
            {
                new StandartOperator(operatorPlus, 0, (x, y) => x + y),
                new StandartOperator(operatorMinus, 0, (x, y) => x - y),
                new StandartOperator(operatorMultiply, 1, (x, y) => x * y),
                new StandartOperator(operatorShare, 1, (x, y) => x / y),
                new StandartOperator(operatorPow, 2, (x, y) => Math.Pow(x, y)),
            };
        }

        public object[] GetOperatorAndConstSortLength()
        {
            List<object> list = new List<object>();
            list.AddRange(StandartOperators);
            list.AddRange(customOperator);
            list.AddRange(consts);

            list.Sort((a, b) => Length(b).CompareTo(Length(a)));

            static int Length(object e)
            {
                if (e.GetType() == typeof(Const))
                    return ((Const)e).Name.Length;
                else
                    return ((IName)e).GetName().Length;
            }

            return list.ToArray();
        }
        public bool AddOperator(CustomOperator customOperator) => AddComponentList(ref this.customOperator, customOperator, ((IName)customOperator).GetName());
        public bool AddOperator(StandartOperator standartOperator) => AddComponentList(ref customOperator, standartOperator, ((IName)standartOperator).GetName());
        public bool AddOperator(EndValueOperator endValueOperator) => AddComponentList(ref customOperator, endValueOperator, ((IName)endValueOperator).GetName());
        public bool RemoveOperator(int index) => RemoveComponentList(ref customOperator, index);
        public bool AddConst(Const @const) => AddComponentList(ref consts, @const, @const.Name);
        public bool RemoveConst(int index) => RemoveComponentList(ref consts, index);
        private bool AddComponentList<T>(ref List<T> ts, T e, string Name)
        {
            if (!CheckUnicalName(Name)) return false;
            ts.Add(e);
            return true;
        }
        private static bool RemoveComponentList<T>(ref List<T> ts, int index)
        {
            if (ts != null && ts.Count > index && index >= 0)
            {
                ts.RemoveAt(index);
                return true;
            }
            else
                return false;
        }
        public bool CheckUnicalName(string Name)
        {
            Name = Name.ToLower();
            return
                (
                (consts == null || consts.LastOrDefault(x => x.Name == Name) == null) &&
                (StandartOperators == null || StandartOperators.LastOrDefault(x => ((IName)x).GetName() == Name) == null) &&
                (customOperator == null || customOperator.LastOrDefault(x => ((IName)x).GetName() == Name) == null) &&
                (Name != operatorSeparatorDouble.ToString() && Name != OperatorParenthesisIn.ToString() && Name != OperatorParenthesisOut.ToString())
                );
        }
        private void UpdateStandartCharOperator(char Old, char New)
        {
            int index = Array.IndexOf(StandartOperators, StandartOperators.Last(x => x.Symbol == Old));
            StandartOperators[index] = new StandartOperator(New, StandartOperators[index].Priority, StandartOperators[index].GetMetotCalculate());
        }
    }
}