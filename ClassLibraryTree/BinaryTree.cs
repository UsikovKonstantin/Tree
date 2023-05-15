using System.Collections.Generic;

namespace ClassLibraryTree
{
    public class BinaryTree
    {
        public Node root;
        public int count;
        string result;

        public BinaryTree()
        {
            count = 0;
            root = null;
        }

        #region Вставка
        public void Insert_R(Node cur, int value)
        {
            if (count == 0)
            {
                root = new Node(value);
                count++;
                return;
            }

            if (value < cur.value)
            {
                if (cur.left == null)
                {
                    count++;
                    cur.left = new Node(value, cur);
                }
                else
                    Insert_R(cur.left, value);
            }
            else
            {
                if (cur.right == null)
                {
                    count++;
                    cur.right = new Node(value, cur);
                }
                else
                    Insert_R(cur.right, value);
            }
        }

        public void Insert_NR(Node cur, int value)
        {
            if (count == 0)
            {
                root = new Node(value);
                count++;
                return;
            }

            count++;
            while (true)
            {
                if (value < cur.value)
                {
                    if (cur.left == null)
                    {
                        cur.left = new Node(value, cur);
                        break;
                    }
                    else
                    {
                        cur = cur.left;
                    }
                }
                else
                {
                    if (cur.right == null)
                    {
                        cur.right = new Node(value, cur);
                        break;
                    }
                    else
                    {
                        cur = cur.right;
                    }
                }
            }
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
            while(true) 
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

        #region Удаление
        public Node Delete(Node cur, int value)
        {
            if (cur == null)
                return null;
            if (value < cur.value)
                cur.left = Delete(cur.left, value);
            else if (value > cur.value)
                cur.right = Delete(cur.right, value);
            else if (cur.left == null || cur.right == null)
                cur = (cur.left == null) ? cur.right : cur.left;
            else
            {
                Node maxInLeft = GetMax_R(cur.left);
                cur.value = maxInLeft.value;
                cur.right = Delete(cur.right, maxInLeft.value);
                cur.left = Delete(cur.left, maxInLeft.value);
            }
            return cur;
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

        #region Удалить дерево
        public void Clear(ref Node cur)
        {
            if (cur != null)
            {
                Clear(ref cur.left);
                Clear(ref cur.right);
                cur = null;
                count--;
            }
        }
        #endregion

        #region Следующий элемент
        public Node Search_Next(Node pTree)
        {
            if (pTree.right != null)
                return GetMin_R(pTree.right);
            Node p1Tree = pTree.parent;
            while (p1Tree != null && pTree == p1Tree.right)
            {
                pTree = p1Tree;
                p1Tree = p1Tree.parent;
            }
            return p1Tree;
        }
        #endregion

        #region Предыдущий элемент
        public Node Search_Prev(Node pTree)
        {
            if (pTree.left != null)
                return GetMax_R(pTree.left);
            Node p1Tree = pTree.parent;
            while (p1Tree != null && pTree == p1Tree.left)
            {
                pTree = p1Tree;
                p1Tree = p1Tree.parent;
            }
            return p1Tree;
        }
        #endregion

        #region Удалить элемент нерекурсивно
        public void Delete_NR(Node pTree, int item)
        {
            Direction d = Direction.L;
            Node parent = null;
            Node cur = pTree;
            while (item > cur.value && cur.right != null || item < cur.value && cur.left != null)
            {
                parent = cur;
                if (item > cur.value)
                {
                    cur = cur.right;
                    d = Direction.R;
                }
                else
                {
                    cur = cur.left;
                    d = Direction.L;
                }
            }
            if (cur != null)
            {
                if (cur.left != null && cur.right != null)
                {
                    parent = cur;
                    Node cur2 = cur.right;
                    while (cur2.left != null)
                    {
                        parent = cur2;
                        cur2 = cur2.left;
                    }
                    cur.value = cur2.value;
                    if (parent != cur)
                        parent.left = cur2.right;
                    else
                        parent.right = cur2.right;
                    cur2 = null;
                }
                else
                {
                    if (cur.left == null)
                    {
                        if (parent != null)
                        {
                            if (d == Direction.L)
                                parent.left = cur.right;
                            else
                                parent.right = cur.right;
                        }
                        else
                        {
                            pTree = cur.right;
                        }
                    }

                    if (cur.right == null)
                    {
                        if (parent != null)
                        {
                            if (d == Direction.L)
                                parent.left = cur.left;
                            else
                                parent.right = cur.left;
                        }
                        else
                        {
                            pTree = cur.left;
                        }
                    }
                }
            }
        }
        #endregion
    }

    public enum Direction
    {
        L, R
    }

    public class Node
    {
        public int value;
        public Node parent;
        public Node left;
        public Node right;
        public int height;

        public Node(int value = 0, Node parent = null, Node left = null, Node right = null)
        {
            this.value = value;
            this.parent = parent;
            this.left = left;
            this.right = right;
            height = 0;
        }
    }
}