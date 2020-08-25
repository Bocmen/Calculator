using Calculator2.Calculator.Data.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator2.Calculator.Data
{
    public static class StringParser
    {
        private static Setting settingStandart = new Setting();
        public static (int StartIndex, object e) SearchOperators(string str, Setting setting)
        {
            object[] OperatorAndConst = setting.GetOperatorAndConstSortLength();
            List<(int StartIndex, object e, int EndIndex)> DataOperatorsIndex = new List<(int StartIndex, object e, int EndIndex)>();
            foreach (var elem in OperatorAndConst)
            {
                var index = GetIndexInStr(str, ((IName)elem).GetName());
                foreach (var i in index)
                {
                    DataOperatorsIndex.Add((i, elem, 0));
                }
            }
            var resul = ComlpeteCostumOperators(DataOperatorsIndex, str);
            return (0, 0);
        }
        private static (int StartIndex, object e, int EndIndex)[] ComlpeteCostumOperators(List<(int StartIndex, object e, int EndIndex)> DataOperatorsIndex, string str)
        {
            // DataOperatorsIndex.Sort((a, b) => a.StartIndex.CompareTo(b.StartIndex));
            List<(int StartIndex, object e, int EndIndex)> ps = new List<(int StartIndex, object e, int EndIndex)>();
            List<(CastumOperator e, int StartIndex)> castumOperators = DataOperatorsIndex.Where(x => x.e.GetType() == typeof(CastumOperator)).Select(x => ((CastumOperator)x.e, x.StartIndex)).ToList();
            for (int i = 0; i < castumOperators.Count; i++)
            {
                CastumOperator @operator = castumOperators[i].e;
                int EndIndex = RemoveStrSearchIndex(castumOperators[i].StartIndex, castumOperators[i].e, str);
                @operator.Data = str.Substring(castumOperators[i].StartIndex + ((IName)castumOperators[i].e).GetName().Length, EndIndex - castumOperators[i].StartIndex - ((IName)castumOperators[i].e).GetName().Length + 1);
                ps.Add((castumOperators[i].StartIndex, castumOperators[i].e, EndIndex));

                int RemoveStrSearchIndex(int StartIndex, CastumOperator @operator, string str)
                {
                    int EndIndex = @operator.GetEndIndexSearch(StartIndex, str);
                    var resulSearch = SearchElemIn(castumOperators[i].StartIndex, EndIndex);
                    if (resulSearch != null && resulSearch.Value.StartIndex != StartIndex)
                    {
                        int IndIndexEraser = RemoveStrSearchIndex(resulSearch.Value.StartIndex, resulSearch.Value.e, str);
                        str = str.Substring(0, resulSearch.Value.StartIndex) + GetStrCount(IndIndexEraser - resulSearch.Value.StartIndex + 1) + str.Remove(0, IndIndexEraser + 1);
                        castumOperators.Remove(resulSearch.Value);
                        return RemoveStrSearchIndex(StartIndex, @operator, str);
                    }
                    return EndIndex;
                }

                string GetStrCount(int Count)
                {
                    string res = string.Empty;
                    for (; Count > 0; Count--)
                        res += " ";
                    return res;
                }

                (CastumOperator e, int StartIndex)? SearchElemIn(int StartIndex, int EndIndex)
                {
                    foreach (var elem in castumOperators)
                        if (elem.StartIndex > StartIndex && elem.StartIndex < EndIndex)
                            return elem;
                    return null;
                }
            }
            return ps.ToArray();
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
    }
}
