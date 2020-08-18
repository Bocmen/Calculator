using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Calculator
{
    public static class Parenthesis
    {
        public static ParenthesisData[] GetData(string str)
        {
            try
            {
                ParenthesisData[] Data = GroupParenthesis(str);
                SetParent(ref Data);
                SetDataChildren(ref Data);
                return Data;
            }
            catch
            {
                throw new Exception("Проверте написание скобок");
            }
        }
        private static void SetDataChildren(ref ParenthesisData[] Data)
        {
            int[] NotParent = SearchIndexElementParent(ref Data, -1);// Data.Select((x, y) => (x, y)).Where(x => x.x.Parent == -1).Select(x => x.y).ToArray();
            for (int i = 0; i < NotParent.Length; i++)
                GetChildren(ref Data, NotParent[i]);
        }
        private static int GetChildren(ref ParenthesisData[] Data, int Parent)
        {
            int[] Children = SearchIndexElementParent(ref Data, Parent); //Data.Select((x, y) => (x, y)).Where(x => x.x.Parent == Parent).Select(x => x.y).ToArray();
            if (Children.Length == 0) return 0;
            int resul = Children.Length;
            for (int i = 0; i < Children.Length; i++)
            {
                resul += GetChildren(ref Data, i);
            }
            Data[Parent].CountChildren = resul;
            return resul;
        }
        private static int[] SearchIndexElementParent(ref ParenthesisData[] Data, int Parent)
        {
            List<int> resul = new List<int>();
            for (int i = 0; i < Data.Length; i++)
                if (Data[i].Parent == Parent) resul.Add(i);
            return resul.ToArray();
        }
        /// <summary>
        /// Установка родителей
        /// </summary>
        private static void SetParent(ref ParenthesisData[] Data)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i].Parent == -1) continue;
                ParenthesisData CopyParenthesis = Data[i];
                CopyParenthesis.Parent = Array.IndexOf(Data, Data.First(x => x.IndexIn == CopyParenthesis.Parent));
                Data[i] = CopyParenthesis;
            }
        }
        /// <summary>
        /// Поиск скобок и первоначальная их групировка
        /// </summary>
        private static ParenthesisData[] GroupParenthesis(string str)
        {
            List<ParenthesisData> resul = new List<ParenthesisData>();

            Stack<int> InS = new Stack<int>();

            // Поиск Частей в скобках
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                    InS.Push(i);
                if (str[i] == ')')
                    resul.Add(
                    new ParenthesisData
                    {
                        IndexIn = InS.Pop(),
                        IndexOut = i,
                        Parent = (InS.Count > 0 ? InS.Peek() : -1)
                    });
            }
            return resul.ToArray();
        }
        public struct ParenthesisData
        {
            public int IndexIn, IndexOut, Parent, CountChildren;
            public double Resul;
        }
    }
}