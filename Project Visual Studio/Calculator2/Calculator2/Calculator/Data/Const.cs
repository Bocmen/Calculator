using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator2.Calculator.Data
{
    public class Const : IName
    {
        /// <summary>
        /// Название
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Значение
        /// </summary>
        public readonly double Value;
        /// <summary>
        /// Инцилизация константы
        /// </summary>
        /// <param name="Name">Название константы</param>
        /// <param name="Value">Значение константы</param>
        public Const(string Name, double Value)
        {
            this.Name = Name.ToLower();
            this.Value = Value;
        }
        public override string ToString()
        {
            return string.Format("Обозначение: {0} значение: {1}", Name, Value);
        }

        string IName.GetName() => Name;
    }
}
