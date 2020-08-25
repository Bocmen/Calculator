/*
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Calculator2.Calculator.Data
{
    public class Setting2
    {
        public char
            OperatorParenthesisIn = '(',
            OperatorParenthesisOut = ')';

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
                operatorPlus = value;
                SetDataStandart();
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
                operatorMinus = value;
                SetDataStandart();
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
                operatorMultiply = value;
                SetDataStandart();
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
                operatorShare = value;
                SetDataStandart();
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
                operatorPow = value;
                SetDataStandart();
            }
        }

        /// <summary>
        /// Стандартные действия калькулятора
        /// </summary>
        public Operator[] Standart { get; private set; }
        /// <summary>
        /// Кастомные операторы
        /// </summary>
        public List<Operator> Costum { get; private set; } = new List<Operator>();
        /// <summary>
        /// Константы к примеру число pi
        /// </summary>
        public List<Const> Consts { get; private set; } = new List<Const>
        {
            new Const("Pi", Math.PI),
            new Const("E", Math.E)
        };
        /// <summary>
        /// Инцилизация класса
        /// </summary>
        public Setting()
        {
            SetDataStandart();
        }
        /// <summary>
        /// Добавить кастомный оператор добавляется в переменную Costum
        /// </summary>
        public void AddOperator(Operator @operator)
        {
            if (Costum == null) Costum = new List<Operator>();
            if (CheckUnicalName(@operator.GetDesignation()))
                Costum.Add(@operator);
            else
                throw new Exception("Невозможно добавить оператор его название не уникальное");
        }
        /// <summary>
        /// Удалить оператор из переменной Costum
        /// </summary>
        public void RemoveOperatorIndex(int index)
        {
            if (Costum != null && Costum.Count > index && index >= 0)
                Costum.RemoveAt(index);
        }
        /// <summary>
        /// Добавить кнстанту в переменную Consts
        /// </summary>
        /// <param name="Name">Название константы</param>
        /// <param name="Value">Значение константы</param>
        public void AddConst(string Name, double Value)
        {
            if (Consts == null) Consts = new List<Const>();
            if (CheckUnicalName(Name))
                Consts.Add(new Const(Name, Value));
            else
                throw new Exception("Невозможно добавить const его название не уникальное");
        }
        /// <summary>
        /// Удалить константу из переменной Consts по её индексу
        /// </summary>
        public void RemoveConstIndex(int index)
        {
            if (Consts != null && Consts.Count > index && index >= 0)
                Consts.RemoveAt(index);
        }
        /// <summary>
        /// Проверка уникальности именования константы или операторы
        /// </summary>
        public bool CheckUnicalName(string Name)
        {
            Name = Name.ToLower();
            if (
                (Consts == null || Consts.LastOrDefault(x => x.Name == Name) == null) &&
                (Standart == null || Standart.LastOrDefault(x => x.GetDesignation() == Name) == null) &&
                (Costum == null || Costum.LastOrDefault(x => x.GetDesignation() == Name) == null)
                )
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Получение всех операторов
        /// </summary>
        public Operator[] GetOperators()
        {
            List<Operator> operators = new List<Operator>();
            operators.AddRange(Standart);
            operators.AddRange(Costum);
            operators.Sort((a, b) => a.GetDesignation().Length.CompareTo(b.GetDesignation().Length) * -1);
            return operators.ToArray();
        }
        /// <summary>
        /// Установка стандартных операторов
        /// </summary>
        private void SetDataStandart()
        {
            Standart = new Operator[]
            {
                new Operator(operatorPlus, 0, (x) => x[0] + x[1]),
                new Operator(operatorMinus, 0, (x) => x[0] - x[1]),
                new Operator(operatorMultiply, 1, (x) => x[0] * x[1]),
                new Operator(operatorShare, 1, (x) => x[0] / x[1]),
                new Operator(operatorPow, 2, (x) => Math.Pow(x[0], x[1])),
            };
        }
    }
}
*/