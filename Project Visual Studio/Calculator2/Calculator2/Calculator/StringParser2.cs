
//using Calculator2.Calculator.Data;
//using Microsoft.VisualBasic.FileIO;
//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Linq;
//using System.Text;

//namespace Calculator2.Calculator
//{
//    public static class StringParser2
//    {
//        private static Setting settingStandart = new Setting();
//        public static object[] Pars(string str, ref Setting setting)
//        {
//            str = str.ToLower();
//            ParsConst(ref str, ref setting);
//            return SearchOperators(str, setting);
//        }
//        private static object[] SearchOperators(string str, Setting setting)
//        {
//            Operator[] operators = setting.GetOperators();
//            List<(int StartIndex, object e, int EndIndex)> DataOperatorsIndex = new List<(int StartIndex, object e, int EndIndex)>();
//            //===================================================== Ищем все операторы
//            foreach (var elem in operators)
//                SearchOperator(elem, elem.GetDesignation(), elem.type == Operator.Type.NElem);
//            // Поиск открывающих скобок
//            SearchOperator(setting.OperatorParenthesisIn, setting.OperatorParenthesisIn.ToString());
//            // Поиск закрывающих скобок
//            SearchOperator(setting.OperatorParenthesisOut, setting.OperatorParenthesisOut.ToString());

//            // Поиск оператора
//            void SearchOperator(object Add, string Search, bool SearchParams = false)
//            {
//                int[] index = GetIndexInStr(str, Search);
//                int Length = Search.Length;
//                foreach (var i in index)
//                {
//                    int EndIndex = i + Length - 1;
//                    if (CheckInOperator(i, EndIndex, ref DataOperatorsIndex))
//                    {
//                        if (!SearchParams)
//                            DataOperatorsIndex.Add((i, Add, EndIndex));
//                        else
//                        {
//                            int CountOperatorParenthesisIn = 0;
//                            EndIndex = i + Search.Length;
//                            do
//                            {
//                                if (str[EndIndex] == setting.OperatorParenthesisIn)
//                                    CountOperatorParenthesisIn++;
//                                else if (str[EndIndex] == setting.OperatorParenthesisOut)
//                                    CountOperatorParenthesisIn--;
//                                EndIndex++;
//                            } while (CountOperatorParenthesisIn != 0);
//                            EndIndex--;
//                            DataOperatorsIndex.Add((i, new OperatorAndData((Operator)Add, str.Substring(i + Search.Length, EndIndex - i - Search.Length + 1)), EndIndex));
//                        }
//                    }
//                }
//            }

//            //====================================================== Ищем числа
//            for (int i = 0; i < str.Length; i++)
//            {
//                string Value = string.Empty;
//                int StartIndex = i;
//                while (i < str.Length && ((Value.Length > 0 && str[i] == ',') || double.TryParse(str[i].ToString(), out double r)))
//                    Value += str[i++];
//                if (!string.IsNullOrEmpty(Value))
//                {
//                    i--;
//                    if (CheckInOperator(StartIndex, i, ref DataOperatorsIndex))
//                        DataOperatorsIndex.Add((StartIndex, double.Parse(Value), i));
//                }
//            }
//            //====================================================== Сортируем в порядке расположения
//            DataOperatorsIndex.Sort((a, b) => a.StartIndex.CompareTo(b.StartIndex));
//            //====================================================== доп. знак + или - указывает на положительный или отрицательный элемент
//            bool IsOldObjectOperator = false;
//            /* Неверно
//            for (int i = 0; i < DataOperatorsIndex.Count; i++)
//            {
//                bool CheckOperatorResul = (DataOperatorsIndex[i].e.GetType() == typeof(Operator) && ((char)((Operator)DataOperatorsIndex[i].e).Designation == setting.OperatorMinus || (char)((Operator)DataOperatorsIndex[i].e).Designation == setting.OperatorPlus));
//                if (IsOldObjectOperator)
//                {
//                    if (DataOperatorsIndex[i].e.GetType() == typeof(Operator) || DataOperatorsIndex[i].e.GetType() == typeof(OperatorAndData) || (DataOperatorsIndex[i].e.GetType() == typeof(char) && (char)DataOperatorsIndex[i].e == setting.OperatorParenthesisIn))
//                    {
//                        if ((char)((Operator)DataOperatorsIndex[i - 1].e).Designation == setting.OperatorMinus)
//                        {
//                            DataOperatorsIndex.RemoveAt(--i);
//                            DataOperatorsIndex.Insert(i, (0, setting.Standart.LastOrDefault(x => (char)x.Designation == setting.OperatorMultiply), 0));
//                            DataOperatorsIndex.Insert(i, (0, -1, 0));
//                            i += 2;
//                        }
//                        else
//                            DataOperatorsIndex.RemoveAt(--i);
//                    }
//                    else
//                    {
//                        if ((char)((Operator)DataOperatorsIndex[i - 1].e).Designation == setting.OperatorMinus)
//                            DataOperatorsIndex[i] = (0, (double)DataOperatorsIndex[i].e * -1, 0);
//                        DataOperatorsIndex.RemoveAt(--i);
//                    }
//                    IsOldObjectOperator = false;
//                }
//                IsOldObjectOperator = CheckOperatorResul;
//            }
//            */
//            return DataOperatorsIndex.Select(x => x.e).ToArray();
//        }
//        private static bool CheckInOperator(int StartIndex, int EndIndex, ref List<(int StartIndex, object e, int EndIndex)> ps)
//        {
//            foreach (var elemOperators in ps)
//            {
//                if (elemOperators.StartIndex <= StartIndex && elemOperators.EndIndex >= EndIndex)
//                    return false;
//            }
//            return true;
//        }
//        private static int[] GetIndexInStr(string str, string Search)
//        {
//            List<int> index = new List<int>();
//            int n = -Search.Length;
//            do
//            {
//                n = str.IndexOf(Search, n + Search.Length);
//                if (n == -1) return index.ToArray();
//                index.Add(n);
//            } while (true);
//        }
//        /// <summary>
//        /// Замена констант на числа
//        /// </summary>
//        private static void ParsConst(ref string str, ref Setting setting)
//        {
//            foreach (var elem in setting.consts)
//                str = str.Replace(elem.Name, elem.Value.ToString());
//        }
//    }
//}