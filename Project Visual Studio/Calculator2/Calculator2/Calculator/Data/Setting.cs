﻿using System;
using System.Collections.Generic;
using System.Text;
using Calculator2.Calculator.Data.Operator;
using System.Linq;

namespace Calculator2.Calculator.Data
{
    public class Setting
    {
        ///////////////////////////////////////////////////////////////// Обозначение (char) стандартных обязательных операторов
        /// <summary>
        /// Обозначение открывающей скобки
        /// </summary>
        public char OperatorParenthesisIn = '(';
        /// <summary>
        /// Обозначение закрывающей скобки
        /// </summary>
        public char OperatorParenthesisOut = ')';

        private char
            operatorPlus = '+',
            operatorMinus = '-',
            operatorMultiply = '*',
            operatorShare = '/',
            operatorPow = '^';

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
        public StandartOperator[] StandartOperators;
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
        private List<object> costumOperator = new List<object>();
        public object[] CostumOperator
        {
            get
            {
                return costumOperator.ToArray();
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
                list.AddRange(costumOperator);
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
            list.AddRange(costumOperator);
            list.AddRange(consts);

            list.Sort((a, b) => Length(b).CompareTo(Length(a)));

            int Length(object e)
            {
                if (e.GetType() == typeof(Const))
                    return ((Const)e).Name.Length;
                else
                    return ((IName)e).GetName().Length;
            }

            return list.ToArray();
        }
        public bool AddOperator(CastumOperator castumOperator) => AddComponentList(ref costumOperator, castumOperator, ((IName)castumOperator).GetName());
        public bool RemoveOperator(int index) => RemoveComponentList(ref costumOperator, index);
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
                (costumOperator == null || costumOperator.LastOrDefault(x => ((IName)x).GetName() == Name) == null)
                );
        }
        private void UpdateStandartCharOperator(char Old, char New)
        {
            int index = Array.IndexOf(StandartOperators, StandartOperators.Last(x => x.Symbol == Old));
            StandartOperators[index] = new StandartOperator(New, StandartOperators[index].Priority, StandartOperators[index].GetMetotCalculate());
        }
    }
}