﻿using Calculator2.Calculator.Data.Operator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator2.Calculator.Data
{
    public static class StringParser
    {
        private static Setting settingStandart = new Setting();

        public static object[] SearchOperators(string str) => SearchOperators(str, settingStandart);
        public static object[] SearchOperators(string str, Setting setting)
        {
            object[] OperatorAndConst = setting.GetOperatorAndConstSortLength();
            List<DataObjectInStr> DataStandartOperatorConstValue = new List<DataObjectInStr>();
            List<DataObjectInStr> DataCostumOperators = new List<DataObjectInStr>();
            foreach (var elem in OperatorAndConst)
            {
                if (elem is CastumOperator)
                    SearchElem(ref DataCostumOperators, elem, ((IName)elem).GetName(), str);
                else if (elem is Const)
                    SearchElem(ref DataStandartOperatorConstValue, ((Const)elem).Value, ((IName)elem).GetName(), str);
                else
                    SearchElem(ref DataStandartOperatorConstValue, elem, ((IName)elem).GetName(), str);
            }
            List<DataObjectInStr> EndResul = ComlpeteCostumOperators(DataCostumOperators, str);
            //Ищем открывающие и закрывающие скобки
            SearchElem(ref DataStandartOperatorConstValue, setting.OperatorParenthesisIn, setting.OperatorParenthesisIn.ToString(), str);
            SearchElem(ref DataStandartOperatorConstValue, setting.OperatorParenthesisOut, setting.OperatorParenthesisOut.ToString(), str);
            //Поиск значений
            SearchValue(ref DataStandartOperatorConstValue, str, setting.OperatorSeparatorDouble);
            EndResul.AddRange(DataStandartOperatorConstValue);
            CompeteOperator(ref EndResul, ref setting);
            return EndResul.Select(x => x.e).ToArray();
        }
        private static void CompeteOperator(ref List<DataObjectInStr> ps, ref Setting setting)
        {
            ps.Sort((a, b) => b.Length.CompareTo(a.Length));
            for (int i = 0; i < ps.Count; i++)
            {
                DataObjectInStr resul = SearchElemIn(ps[i].StartIndex, ps[i].EndIndex, ref ps);
                while (resul != null)
                {
                    ps.Remove(resul);
                    resul = SearchElemIn(ps[i].StartIndex, ps[i].EndIndex, ref ps);
                }
            }
            ps.Sort((a, b) => a.StartIndex.CompareTo(b.StartIndex));
            PlusMinusAddValue(ref ps, setting);
            JoinValueAndEndValueOperator(ref ps, setting.OperatorParenthesisIn, setting.OperatorParenthesisOut);
        }
        private static void PlusMinusAddValue(ref List<DataObjectInStr> ps, Setting setting)
        {
            bool OldSymbolOperaor = true;
            for (int i = 0; i < ps.Count; i++)
            {
                if (CheckPlusMinus(ps[i].e) && OldSymbolOperaor && (i + 1) < ps.Count && CheckCastumOperatorAndValue(ps[i + 1].e))
                {
                    if (ps[i + 1].e is double D)
                    {
                        if (CheckOperatorMinus(ps[i].e))
                            ps[i + 1].e = D * -1;
                        ps.RemoveAt(i--);
                    }
                    else
                    {
                        bool CheckOperatorMinusResul = CheckOperatorMinus(ps[i].e);
                        ps.RemoveAt(i);
                        if (CheckOperatorMinusResul)
                        {
                            ps.Insert(i, new DataObjectInStr(0, setting.StandartOperators.First(x => x.Symbol == setting.OperatorMultiply), 0));
                            ps.Insert(i, new DataObjectInStr(0, (double)-1, 0));
                        }
                        i++;
                        OldSymbolOperaor = false;
                    }
                    bool CheckOperatorMinus(object e) => (e is StandartOperator @operator) && @operator.Symbol == setting.OperatorMinus;
                }
                else
                {
                    OldSymbolOperaor = CheckPlusMinus(ps[i].e);
                }
                bool CheckCastumOperatorAndValue(object e) => e is CastumOperator || e is double || (e is char @char && @char == setting.OperatorParenthesisIn);
                bool CheckPlusMinus(object e) => (e is StandartOperator @operator) && (@operator.Symbol == setting.OperatorPlus || @operator.Symbol == setting.OperatorMinus);
            }
        }
        private static void JoinValueAndEndValueOperator(ref List<DataObjectInStr> ps, char OperatorParenthesisIn, char OperatorParenthesisOut)
        {
            for (int i = 0; i < ps.Count; i++)
            {
                if (ps[i].e is EndValueOperator @operator && (i - 1) < ps.Count)
                {
                    if (ps[i - 1].e is double @D)
                    {
                        @operator.Data = @D;
                        ps.RemoveAt(i++ - 1);
                    }
                    else if (ps[i - 1].e is CastumOperator castumOperator)
                    {
                        @operator.Data = castumOperator;
                        ps.RemoveAt(i++ - 1);
                    }
                    else if (CheckChar(ps[i - 1].e, OperatorParenthesisOut))
                    {
                        int count = 1;
                        int endIndex = 2;
                        do
                        {
                            if (i - endIndex < 0) break;

                            if (CheckChar(ps[i - endIndex].e, OperatorParenthesisOut))
                                count++;
                            if (CheckChar(ps[i - endIndex].e, OperatorParenthesisIn))
                                count--;
                            endIndex++;

                        } while (count != 0);
                        endIndex = i - --endIndex;
                        @operator.Data = ps.Where((x, y) => y >= endIndex && y < i).ToArray();
                        for (int Del = i - 1; Del >= endIndex; Del--)
                            ps.RemoveAt(Del);
                        i = endIndex;
                    }
                    bool CheckChar(object e, char CheckChar) => e is char @char && @char == CheckChar;
                }
            }
        }
        private static void SearchValue(ref List<DataObjectInStr> DataStandartOperatorConstValue, string str, char Separator)
        {
            for (int i = 0; i < str.Length; i++)
            {
                string Value = string.Empty;
                int StartIndex = i;
                while (i < str.Length && ((Value.Length > 0 && str[i] == Separator) || double.TryParse(str[i].ToString(), out double r)))
                {
                    if (str[i] == Separator)
                        Value += ',';
                    else
                        Value += str[i];
                    i++;
                }
                if (!string.IsNullOrEmpty(Value))
                {
                    DataStandartOperatorConstValue.Add(new DataObjectInStr(StartIndex, double.Parse(Value), 0));
                }
            }
        }
        private static void SearchElem(ref List<DataObjectInStr> DataOperatorsIndex, object elem, string Name, string str)
        {
            var index = GetIndexInStr(str, Name);
            foreach (var i in index)
                DataOperatorsIndex.Add(new DataObjectInStr(i, elem, 0));
        }
        private static List<DataObjectInStr> ComlpeteCostumOperators(List<DataObjectInStr> castumOperators, string str)
        {
            List<DataObjectInStr> ps = new List<DataObjectInStr>();
            for (int i = 0; i < castumOperators.Count; i++)
            {
                CastumOperator @operator = (CastumOperator)castumOperators[i].e;
                int EndIndex = RemoveStrSearchIndex(castumOperators[i].StartIndex, @operator, str);
                @operator.Data = str.Substring(castumOperators[i].StartIndex + ((IName)castumOperators[i].e).GetName().Length, EndIndex - castumOperators[i].StartIndex - ((IName)castumOperators[i].e).GetName().Length + 1);
                ps.Add(new DataObjectInStr(castumOperators[i].StartIndex, castumOperators[i].e, EndIndex));

                int RemoveStrSearchIndex(int StartIndex, CastumOperator @operator, string str)
                {
                    int EndIndex = @operator.GetEndIndexSearch(StartIndex, str);
                    var resulSearch = SearchElemIn(castumOperators[i].StartIndex, EndIndex, ref castumOperators);

                    if (resulSearch != null && resulSearch.StartIndex != StartIndex && @operator.HelperSearchIndex)
                    {
                        int IndIndexEraser = RemoveStrSearchIndex(resulSearch.StartIndex, (CastumOperator)resulSearch.e, str);
                        str = str.Substring(0, resulSearch.StartIndex) + GetStrCount(IndIndexEraser - resulSearch.StartIndex + 1) + str.Remove(0, IndIndexEraser + 1);
                        castumOperators.Remove(resulSearch);
                        return RemoveStrSearchIndex(StartIndex, @operator, str);
                    }
                    return EndIndex;
                }

                static string GetStrCount(int Count)
                {
                    string res = string.Empty;
                    for (; Count > 0; Count--)
                        res += " ";
                    return res;
                }
            }
            return ps;
        }
        private static DataObjectInStr SearchElemIn(int StartIndex, int EndIndex, ref List<DataObjectInStr> dataObjectInStrs)
        {
            foreach (var elem in dataObjectInStrs)
                if (elem.StartIndex > StartIndex && elem.StartIndex < EndIndex)
                    return elem;
            return null;
        }
        private static int[] GetIndexInStr(string str, string Search)
        {
            List<int> index = new List<int>();
            int n = -Search.Length;
            do
            {
                n = str.IndexOf(Search, n + Search.Length);
                if (n == -1) return index.ToArray();
                index.Add(n);
            } while (true);
        }
        private class DataObjectInStr
        {
            public int StartIndex;
            public int EndIndex;
            public object e;
            public int Length
            {
                get
                {
                    return EndIndex != 0 ? (EndIndex - StartIndex + 1) : 1; ;
                }
                private set { }
            }

            public DataObjectInStr(int StartIndex, object e, int EndIndex)
            {
                this.StartIndex = StartIndex;
                this.e = e;
                this.EndIndex = EndIndex;
            }

            public override string ToString() => GetName();
            private string GetName()
            {
                if (e is double @double)
                    return @double.ToString();
                else if (e is IName @IName)
                    return @IName.GetName();
                else
                    return ((char)e).ToString();
            }
        }
    }
}