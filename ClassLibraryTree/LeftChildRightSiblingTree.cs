using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTree
{
    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightSibling { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            LeftChild = null;
            RightSibling = null;
        }
    }

    public class LeftChildRightSiblingTree
    {
        public TreeNode Root { get; set; }

        public LeftChildRightSiblingTree()
        {
            Root = null;
        }

        public void AddChild(TreeNode parent, TreeNode child)
        {
            if (parent.LeftChild == null)
            {
                parent.LeftChild = child;
            }
            else
            {
                TreeNode sibling = parent.LeftChild;
                while (sibling.RightSibling != null)
                {
                    sibling = sibling.RightSibling;
                }
                sibling.RightSibling = child;
            }
        }

        public void Traverse(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Console.Write(node.Value + " ");

            // Рекурсивно обходим всех левых детей
            Traverse(node.LeftChild);

            // Рекурсивно обходим всех правых соседей
            Traverse(node.RightSibling);
        }
    }
}
