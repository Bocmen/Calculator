using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Data
{
    /// <summary>
    /// Данные об операторе (для взаимодействия с двумя числами)
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// Его обозначение в виде символа
        /// </summary>
        public readonly char Symbol;
        /// <summary>
        /// Его приоритет
        /// </summary>
        public readonly byte Priority;
        /// <summary>
        /// Инцилизация оператора
        /// </summary>
        /// <param name="Symbol">Символьное обозначение</param>
        public Operator(char Symbol, byte Priority)
        {
            this.Symbol = Symbol;
            this.Priority = Priority;
        }
        public override string ToString()
        {
            return string.Format("Символ: {0} Приоритет: {1}", Symbol, Priority);
        }
    }
}
