/*
using System;

namespace Calculator2.Calculator.Data
{
    

    public class Operator
    {
        public delegate double MetotCalculate(params object[] vs);

        /// <summary>
        /// Обозначение
        /// </summary>
        public readonly object Designation;
        /// <summary>
        /// Приоритет
        /// </summary>
        public readonly byte? Priority;


        /// <summary>
        /// Метод выполняющий подсчёт
        /// </summary>
        private readonly MetotCalculate metotCalculate;
        /// <summary>
        /// Количество входных параметров у метода
        /// </summary>
        private readonly byte? CountParams;

        /// <summary>
        /// Инцилизация односимвольного оператора
        /// Тип присваивается TwoElemStandart (между двумя элементами к примеру оператор +)
        /// </summary>
        /// <param name="Designation">Обозначение</param>
        /// <param name="Priority">Приоритет</param>
        /// <param name="metotCalculate">Метод высчитывающий значение</param>
        public Operator(char Designation, byte Priority, MetotCalculate metotCalculate)
        {
            this.Designation = Designation.ToString().ToLower()[0];
            this.Priority = Priority;
            type = Type.TwoElemStandart;
            this.metotCalculate = metotCalculate;
            CountParams = 2;
        }
        /// <summary>
        /// Инцилизация оператора работающего с N кол-во элементов к примеру Sin(30) или log(2,4) или Max(1, 2, 3, 4, 5)
        /// </summary>
        /// <param name="Designation">Обозначение</param>
        /// <param name="metotCalculate">Метод выполняющий действия</param>
        /// <param name="CountParams">Кол во входных параметров (если null то любое кол-во)</param>
        public Operator(string Designation, MetotCalculate metotCalculate, byte? CountParams = null)
        {
            this.Designation = Designation.ToLower();
            type = Type.NElem;
            this.metotCalculate = metotCalculate;
            this.CountParams = CountParams;
        }
        /// <summary>
        /// Расчёт значение с использованием данного оператора
        /// </summary>
        public double Calculate(params double[] vs)
        {
            if (CountParams != null && vs.Length != CountParams) throw new Exception("Слишком много параметров");
            return metotCalculate.Invoke(vs);
        }
        /// <summary>
        /// Получить обозначение оператора в виде string
        /// </summary>
        public string GetDesignation()
        {
            if (Designation.GetType() == typeof(string))
                return (string)Designation;
            else
                return ((char)Designation).ToString();
        }

        public override string ToString()
        {
            return string.Format("Обозначение: {0}{1} Тип: {2}{3}",
                GetDesignation(),
                Priority != null ? string.Format(" Приоритет: {0}", Priority) : null,
                type.ToString(),
                string.Format(" Кол-во входных параметров: {0}", CountParams == null ? "null" : CountParams.ToString())
                );
        }

    }
    public class OperatorAndData
    {
        public readonly Operator @operator;
        public readonly object data;

        public OperatorAndData(Operator @operator, object data)
        {
            this.@operator = @operator;
            this.data = data;
        }
    }
}
*/