/*using Calculator2.Calculator.Data.Operator;
using System;
using System.Collections.Generic;
using System.Text;
using static Calculator2.Calculator.Data.StringParser;

namespace Calculator2.Calculator.Data
{
    public class Test
    {
        private static Setting settingStandart = new Setting();
        public static object[] SearchOperators(string str, Setting setting)
        {
            object[] OperatorAndConst = setting.GetOperatorAndConstSortLength();
            List<DataObjectInStr> DataStandartOperatorConstValue = new List<DataObjectInStr>();
            List<DataObjectInStr> DataCostumOperators = new List<DataObjectInStr>();
            foreach (var elem in OperatorAndConst)
            {
                if (elem is CastumOperator)
                    SearchElem(ref DataCostumOperators, elem, ((IName)elem).GetName(), str);
                else
                    SearchElem(ref DataStandartOperatorConstValue, elem, ((IName)elem).GetName(), str);
            }
            List<DataObjectInStr> EndResul = ComlpeteCostumOperators(DataCostumOperators, str);
            //Ищем открывающие и закрывающие скобки
            SearchElem(ref DataStandartOperatorConstValue, setting.OperatorParenthesisIn, setting.OperatorParenthesisIn.ToString(), str);
            SearchElem(ref DataStandartOperatorConstValue, setting.OperatorParenthesisOut, setting.OperatorParenthesisOut.ToString(), str);
            SearchValue(ref DataStandartOperatorConstValue, str, setting.OperatorSeparatorDouble);
            EndResul.AddRange(DataStandartOperatorConstValue);
            CompeteOperator(EndResul);
            return null;
        }
        private static void CompeteOperator(List<DataObjectInStr> ps)
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
            //TOODO применение знака отрицания к элементам
            //TOODO разрешить включать и выключать помощь при поиски конца сложного оператора
            Console.WriteLine("End");
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
            for (int i = castumOperators.Count - 1; i > 0; i--)
            {
                CastumOperator @operator = (CastumOperator)castumOperators[i].e;
                int EndIndex = RemoveStrSearchIndex(castumOperators[i].StartIndex, @operator, str);
                int StartIndex = @operator.InverseSearchEndIndex ? EndIndex : castumOperators[i].StartIndex;
                if (@operator.InverseSearchEndIndex)
                    EndIndex = castumOperators[i].StartIndex;

                @operator.Data = str.Substring(StartIndex + ((IName)castumOperators[i].e).GetName().Length, EndIndex - StartIndex - ((IName)castumOperators[i].e).GetName().Length + 1);

                ps.Add(new DataObjectInStr(castumOperators[i].StartIndex, castumOperators[i].e, EndIndex));

                int RemoveStrSearchIndex2(int StartIndex, CastumOperator @operator, string str)
                {
                    int EndIndex = @operator.InverseSearchEndIndex ? StartIndex : @operator.GetEndIndexSearch(StartIndex, str);
                    if (@operator.InverseSearchEndIndex)
                        StartIndex = @operator.GetEndIndexSearch(StartIndex, str);

                    DataObjectInStr resulSearch = SearchElemIn(StartIndex, EndIndex, ref castumOperators);

                    if (resulSearch != null && resulSearch.StartIndex != StartIndex && resulSearch.StartIndex != EndIndex)
                    {
                        int IndIndexEraser = RemoveStrSearchIndex2(resulSearch.StartIndex, (CastumOperator)resulSearch.e, str);

                    }
                    return 0;
                }

                int RemoveStrSearchIndex(int StartIndex, CastumOperator @operator, string str)
                {
                    int EndIndex = @operator.GetEndIndexSearch(StartIndex, str);
                    StartIndex = @operator.InverseSearchEndIndex ? EndIndex : castumOperators[i].StartIndex;
                    if (@operator.InverseSearchEndIndex)
                        EndIndex = castumOperators[i].StartIndex;

                    var resulSearch = SearchElemIn(StartIndex, EndIndex, ref castumOperators);
                    if (resulSearch != null && resulSearch.StartIndex != StartIndex && resulSearch.StartIndex != EndIndex)
                    {
                        int IndIndexEraser = RemoveStrSearchIndex(resulSearch.StartIndex, (CastumOperator)resulSearch.e, str);
                        str = str.Substring(0, resulSearch.StartIndex) + GetStrCount(IndIndexEraser - resulSearch.StartIndex + 1) + str.Remove(0, IndIndexEraser + 1);
                        castumOperators.Remove(resulSearch);
                        return RemoveStrSearchIndex(StartIndex, @operator, str);
                    }
                    return @operator.InverseSearchEndIndex ? StartIndex : EndIndex;
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
        public class DataObjectInStr
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
                if (e is double)
                    return ((double)e).ToString();
                else if (e is IName)
                    return ((IName)e).GetName();
                else
                    return ((char)e).ToString();
            }
        }
    }
}
*/