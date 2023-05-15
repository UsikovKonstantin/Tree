using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTree
{
    public class AVLTree
    {
        public Node root;
        public int count;
        string result;
        public AVLTree()
        {
            count = 0;
            root = null;
            result = "";
        }

        #region Балансировка
        public void UpdateHeight(Node node)
        {
            node.height = Math.Max(GetHeight(node.left), GetHeight(node.right)) + 1;
        }

        public int GetHeight(Node node)
        {
            return node == null ? -1 : node.height;
        }

        public int GetBalance(Node node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.right) - GetHeight(node.left);
        }

        public void Swap(Node a, Node b)
        {
            int a_value = a.value;
            a.value = b.value;
            b.value = a_value;
        }

        public void RightRotate(Node node)
        {
            Swap(node, node.left);
            Node buffer = node.right;
            node.right = node.left;
            node.left = node.right.left;
            node.right.left = node.right.right;
            node.right.right = buffer;
            UpdateHeight(node.right);
            UpdateHeight(node);
        }

        public void LeftRotate(Node node)
        {
            Swap(node, node.right);
            Node buffer = node.left;
            node.left = node.right;
            node.right = node.left.right;
            node.left.right = node.left.left;
            node.left.left = buffer;
            UpdateHeight(node.left);
            UpdateHeight(node);
        }

        public void Balance(Node node)
        {
            int balance = GetBalance(node);
            if (balance == -2)
            {
                if (GetBalance(node.left) == 1)
                    LeftRotate(node.left);
                RightRotate(node);
            }
            else if (balance == 2)
            {
                if (GetBalance(node.right) == -1)
                    RightRotate(node.right);
                LeftRotate(node);
            }
        }
        #endregion

        #region Вставка
        public void Insert(Node node, int value)
        {
            if (count == 0)
            {
                root = new Node(value);
                count++;
                return;
            }

            if (value < node.value)
            {
                if (node.left == null)
                {
                    node.left = new Node(value);
                    count++;
                }
                else
                    Insert(node.left, value);
            }
            else
            {
                if (node.right == null)
                {
                    node.right = new Node(value);
                    count++;
                }
                else
                    Insert(node.right, value);
            }
            UpdateHeight(node);
            Balance(node);
        }
        #endregion

        #region Удаление
        public Node Delete(Node node, int value)
        {
            Node deleted = DeleteMain(node, value);
            if (deleted != null)
                count--;
            return deleted;
        }

        public Node DeleteMain(Node node, int value)
        {
            if (node == null)
                return null;
            else if (value < node.value)
                node.left = DeleteMain(node.left, value);
            else if (value > node.value)
                node.right = DeleteMain(node.right, value);
            else
            {
                if (node.left == null || node.right == null)
                    node = (node.left == null) ? node.right : node.left;
                else
                {
                    Node maxInLeft = GetMax_R(node.left);
                    node.value = maxInLeft.value;
                    node.right = DeleteMain(node.right, maxInLeft.value);
                    node.left = DeleteMain(node.left, maxInLeft.value);
                }
            }

            if (node != null)
            {
                UpdateHeight(node);
                Balance(node);
            }

            return node;
        }
        #endregion

        #region Поиск
        public Node Search_R(Node cur, int value)
        {
            if (cur == null)
                return null;
            if (cur.value == value)
                return cur;
            if (value < cur.value)
                return Search_R(cur.left, value);
            else
                return Search_R(cur.right, value);
        }

        public Node Search_NR(Node cur, int value)
        {
            while (true)
            {
                if (cur == null)
                    return null;
                if (cur.value == value)
                    return cur;
                if (value < cur.value)
                    cur = cur.left;
                else
                    cur = cur.right;
            }
        }
        #endregion

        #region Минимум
        public Node GetMin_R(Node cur)
        {
            if (cur == null)
                return null;
            if (cur.left == null)
                return cur;
            return GetMin_R(cur.left);
        }

        public Node GetMin_NR(Node cur)
        {
            if (cur == null)
                return null;
            while (true)
            {
                if (cur.left == null)
                    return cur;
                cur = cur.left;
            }
        }
        #endregion

        #region Максимум
        public Node GetMax_R(Node cur)
        {
            if (cur == null)
                return null;
            if (cur.right == null)
                return cur;
            return GetMax_R(cur.right);
        }

        public Node GetMax_NR(Node cur)
        {
            if (cur == null)
                return null;
            while (true)
            {
                if (cur.right == null)
                    return cur;
                cur = cur.right;
            }
        }
        #endregion

        #region Обход в прямом порядке
        public string PreOrder_R(Node cur)
        {
            result = "";
            PreOrder_R_Main(cur);
            return result;
        }
        public void PreOrder_R_Main(Node cur)
        {
            if (cur == null)
                return;
            result += cur.value + " ";
            PreOrder_R_Main(cur.left);
            PreOrder_R_Main(cur.right);
        }

        public string PreOrder_NR(Node cur)
        {
            if (cur == null)
                return "";
            result = "";

            Stack<Node> st = new Stack<Node>();
            while (true)
            {
                if (cur != null)
                {
                    result += cur.value + " ";
                    st.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (st.Count == 0)
                        return result;
                    cur = st.Pop();
                    cur = cur.right;
                }
            }
        }
        #endregion

        #region Обход в обратном порядке
        public string PostOrder_R(Node cur)
        {
            result = "";
            PostOrder_R_Main(cur);
            return result;
        }
        public void PostOrder_R_Main(Node cur)
        {
            if (cur == null)
                return;
            PostOrder_R_Main(cur.left);
            PostOrder_R_Main(cur.right);
            result += cur.value + " ";
        }

        public string PostOrder_NR(Node cur)
        {
            if (cur == null)
                return "";
            result = "";
            Stack<Node> st = new Stack<Node>();
            st.Push(cur);

            while (st.Count != 0)
            {
                Node next = st.Peek();

                bool finishedSubtrees = (next.right == cur || next.left == cur);
                bool isLeaf = (next.left == null && next.right == null);
                if (finishedSubtrees || isLeaf)
                {
                    st.Pop();
                    result += next.value + " ";
                    cur = next;
                }
                else
                {
                    if (next.right != null)
                        st.Push(next.right);
                    if (next.left != null)
                        st.Push(next.left);
                }
            }
            return result;
        }
        #endregion

        #region Обход в симметричном порядке
        public string InOrder_R(Node cur)
        {
            result = "";
            InOrder_R_Main(cur);
            return result;
        }
        public void InOrder_R_Main(Node cur)
        {
            if (cur == null)
                return;
            InOrder_R_Main(cur.left);
            result += cur.value + " ";
            InOrder_R_Main(cur.right);
        }

        public string InOrder_NR(Node cur)
        {
            if (cur == null)
                return "";
            result = "";

            Stack<Node> st = new Stack<Node>();
            while (true)
            {
                if (cur != null)
                {
                    st.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (st.Count == 0)
                        return result;
                    cur = st.Pop();
                    result += cur.value + " ";
                    cur = cur.right;
                }
            }
        }
        #endregion
    }
}