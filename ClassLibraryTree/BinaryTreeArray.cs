using System;
using System.Collections.Generic;

namespace ClassLibraryTree
{
    public class BinaryTreeArray
    {
        public const int INF = 1000000000;
        public int[] arr;
        public int count;
        public string result;
        public int capacity;
        public int levels;

        public BinaryTreeArray()
        {
            capacity = 0;
            arr = new int[0];
            count = 0;
            levels = 0;
        }

        #region Вставка
        public void Insert_R(int cur, int value)
        {
            if (count == 0)
            {
                arr = new int[1];
                capacity = 1;
                levels = 1;
                arr[0] = value; 
                count++;
                return;
            }

            if (cur >= arr.Length)
            {
                levels++;
                int[] arrNew = new int[2 * capacity + 1];
                for (int i = 0; i < capacity; i++)
                    arrNew[i] = arr[i];
                for (int i = capacity; i < 2 * capacity + 1; i++)
                    arrNew[i] = INF;
                arr = arrNew;
                capacity = 2 * capacity + 1;
            }

            if (arr[cur] == INF)
            {
                count++;
                arr[cur] = value;
                return;
            }

            if (value < arr[cur])
                Insert_R(2 * cur + 1, value);
            else
                Insert_R(2 * cur + 2, value);
        }

        public void Insert_NR(int cur, int value)
        {
            if (count == 0)
            {
                arr = new int[1];
                capacity = 1;
                arr[0] = value;
                count++;
                return;
            }

            count++;
            while (true)
            {
                if (cur >= arr.Length)
                {
                    levels++;
                    int[] arrNew = new int[2 * capacity + 1];
                    for (int i = 0; i < capacity; i++)
                        arrNew[i] = arr[i];
                    for (int i = capacity; i < 2 * capacity + 1; i++)
                        arrNew[i] = INF;
                    arr = arrNew;
                    capacity = 2 * capacity + 1;
                }

                if (arr[cur] == INF)
                {
                    arr[cur] = value;
                    return;
                }

                if (value < arr[cur])
                    cur = 2 * cur + 1;
                else
                    cur = 2 * cur + 2;
            }
        }
        #endregion

        #region Поиск
        public int Search_R(int cur, int value)
        {
            if (arr[cur] == INF)
                return -1;
            if (arr[cur] == value)
                return cur;
            if (value < arr[cur])
                return Search_R(2 * cur + 1, value);
            else
                return Search_R(2 * cur + 2, value);
        }

        public int Search_NR(int cur, int value)
        {
            while (true)
            {
                if (arr[cur] == INF)
                    return -1;
                if (arr[cur] == value)
                    return cur;
                if (value < arr[cur])
                    cur = 2 * cur + 1;
                else
                    cur = 2 * cur + 2;
            }
        }
        #endregion

        #region Минимум
        public int GetMin_R(int cur)
        {
            return arr[GetMin_R_Main(cur)];
        }
        public int GetMin_R_Main(int cur)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return INF;
            if (2 * cur + 1 >= arr.Length || arr[2 * cur + 1] == INF)
                return cur;
            return GetMin_R_Main(2 * cur + 1);
        }

        public int GetMin_NR(int cur)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return INF;
            while (true)
            {
                if (2 * cur + 1 >= arr.Length || arr[2 * cur + 1] == INF)
                    return arr[cur];
                cur = 2 * cur + 1;
            }
        }
        #endregion

        #region Максимум
        public int GetMax_R(int cur)
        {
            return arr[GetMax_R_Main(cur)];
        }
        public int GetMax_R_Main(int cur)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return -1;
            if (2 * cur + 2 >= arr.Length || arr[2 * cur + 2] == INF)
                return cur;
            return GetMax_R_Main(2 * cur + 2);
        }

        public int GetMax_NR(int cur)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return -1;
            while (true)
            {
                if (2 * cur + 2 >= arr.Length || arr[2 * cur + 2] == INF)
                    return arr[cur];
                cur = 2 * cur + 2;
            }
        }
        #endregion

        #region Удаление
        public void Delete(int cur, int value)
        {
            DeleteMain(cur, value);
            bool f = false;
            for (int i = (int)Math.Pow(2, levels - 1) - 1; i < (int)Math.Pow(2, levels) - 1; i++)
                if (arr[i] != INF)
                    f = true;
            if (!f)
            {
                levels--;
                int[] newArr = new int[(int)Math.Pow(2, levels) - 1];
                capacity = newArr.Length;
                for (int i = 0; i < newArr.Length; i++)
                    newArr[i] = arr[i]; 
                arr = newArr;
            }
        }

        public int DeleteMain(int cur, int value)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return cur;
            if (value < arr[cur])
            {
                if (2 * cur + 1 < arr.Length)
                {
                    arr[2 * cur + 1] = arr[DeleteMain(2 * cur + 1, value)];
                }
            }
            else if (value > arr[cur])
            {
                if (2 * cur + 2 < arr.Length)
                {
                    arr[2 * cur + 2] = arr[DeleteMain(2 * cur + 2, value)];
                }
            }
            else if (2 * cur + 1 >= arr.Length || arr[2 * cur + 1] == INF || 2 * cur + 2 >= arr.Length || arr[2 * cur + 2] == INF)
            {
                count--;
                if (2 * cur + 1 >= arr.Length || arr[2 * cur + 1] == INF)
                {
                    if (2 * cur + 2 < arr.Length)
                    {
                        arr[cur] = arr[2 * cur + 2];
                        arr[2 * cur + 2] = INF;
                    }
                    else
                    {
                        arr[cur] = INF;
                    }
                }
                else
                {
                    if (2 * cur + 1 < arr.Length)
                    {
                        arr[cur] = arr[2 * cur + 1];
                        arr[2 * cur + 1] = INF;
                    }
                    else
                    {
                        arr[cur] = INF;
                    }
                }
            }
            else
            {
                int maxInLeft = GetMax_R(2 * cur + 1);
                arr[cur] = maxInLeft;
                arr[2 * cur + 2] = arr[DeleteMain(2 * cur + 2, maxInLeft)];
                arr[2 * cur + 1] = arr[DeleteMain(2 * cur + 1, maxInLeft)];
            }
            return cur;
        }
        #endregion
     
        #region Обход в прямом порядке
        public string PreOrder_R(int cur)
        {
            result = "";
            PreOrder_R_Main(cur);
            return result;
        }
        public void PreOrder_R_Main(int cur)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return;
            result += arr[cur] + " ";
            PreOrder_R_Main(2 * cur + 1);
            PreOrder_R_Main(2 * cur + 2);
        }

        public string PreOrder_NR(int cur)
        {
            if (arr[cur] == INF)
                return "";
            result = "";

            Stack<int> st = new Stack<int>();
            while (true)
            {
                if (cur < arr.Length && arr[cur] != INF)
                {
                    result += arr[cur] + " ";
                    st.Push(cur);
                    cur = 2 * cur + 1;
                }
                else
                {
                    if (st.Count == 0)
                        return result;
                    cur = st.Pop();
                    cur = 2 * cur + 2;
                }
            }
        }
        #endregion

        #region Обход в обратном порядке
        public string PostOrder_R(int cur)
        {
            result = "";
            PostOrder_R_Main(cur);
            return result;
        }
        public void PostOrder_R_Main(int cur)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return;
            PostOrder_R_Main(2 * cur + 1);
            PostOrder_R_Main(2 * cur + 2);
            result += arr[cur] + " ";
        }

        public string PostOrder_NR(int cur)
        {
            if (arr[cur] == INF)
                return "";
            result = "";
            Stack<int> st = new Stack<int>();
            st.Push(cur);

            while (st.Count != 0)
            {
                int next = st.Peek();

                bool finishedSubtrees = (2 * next + 2 == cur || 2 * next + 1 == cur);
                bool isLeaf = ((2 * next + 1 >= arr.Length || arr[2 * next + 1] == INF) && (2 * next + 2 >= arr.Length || arr[2 * next + 2] == INF));
                if (finishedSubtrees || isLeaf)
                {
                    st.Pop();
                    result += arr[next] + " ";
                    cur = next;
                }
                else
                {
                    if (arr[2 * next + 2] != INF)
                        st.Push(2 * next + 2);
                    if (arr[2 * next + 1] != INF)
                        st.Push(2 * next + 1);
                }
            }
            return result;
        }
        #endregion

        #region Обход в симметричном порядке
        public string InOrder_R(int cur)
        {
            result = "";
            InOrder_R_Main(cur);
            return result;
        }
        public void InOrder_R_Main(int cur)
        {
            if (cur >= arr.Length || arr[cur] == INF)
                return;
            InOrder_R_Main(2 * cur + 1);
            result += arr[cur] + " ";
            InOrder_R_Main(2 * cur + 2);
        }

        public string InOrder_NR(int cur)
        {
            if (arr[cur] == INF)
                return "";
            result = "";

            Stack<int> st = new Stack<int>();
            while (true)
            {
                if (cur < arr.Length && arr[cur] != INF)
                {
                    st.Push(cur);
                    cur = 2 * cur + 1;
                }
                else
                {
                    if (st.Count == 0)
                        return result;
                    cur = st.Pop();
                    result += arr[cur] + " ";
                    cur = 2 * cur + 2;
                }
            }
        }
        #endregion
    }
}