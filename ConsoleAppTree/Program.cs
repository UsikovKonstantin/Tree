using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryTree;

namespace ConsoleAppTree
{
    internal class Program
    {
        static void Main(string[] args)
        {

            BinaryTree binTree = new BinaryTree();
            List<int> items = new List<int>() { 21, 7, 32, 5, 14, 27, 37, 4, 6, 12, 18, 25, 30, 34, 39, 2, 9, 24, 33 };
            //List<int> items = new List<int>() { 11, 9, 2, 4, 1, 7, 14, 15, 10, 13, 12, 3, 5, 8, 6 };
            foreach (int item in items)
                binTree.Insert_R(binTree.root, item);

            Console.WriteLine(binTree.InOrder_R(binTree.root));
            //Console.WriteLine(binTree.InOrder_NR(0));
            //Console.WriteLine(binTree.PreOrder_R(0));
            //Console.WriteLine(binTree.PreOrder_NR(0));
            //Console.WriteLine(binTree.PostOrder_R(0));
            //Console.WriteLine(binTree.PostOrder_NR(0));



            /*
            int[] keys = { 1, 2, 3 };
            double[] probabilities = { 60, 30, 10 };
            OptimalBSTNode root = OptimalBST.BuildOptimalBST(keys, probabilities);
            OptimalBST.InOrderTraversal(root); 
            */

            /*
            // Создаем дерево
            LeftChildRightSiblingTree tree = new LeftChildRightSiblingTree();

            // Создаем узлы
            TreeNode root = new TreeNode(1);
            TreeNode node2 = new TreeNode(2);
            TreeNode node3 = new TreeNode(3);
            TreeNode node4 = new TreeNode(4);
            TreeNode node5 = new TreeNode(5);
            TreeNode node6 = new TreeNode(6);

            // Строим структуру дерева
            tree.Root = root;
            tree.AddChild(root, node2);
            tree.AddChild(root, node3);
            tree.AddChild(node2, node4);
            tree.AddChild(node2, node5);
            tree.AddChild(node3, node6);

            // Обходим дерево
            tree.Traverse(tree.Root);
            // Вывод: 1 2 4 5 3 6
            */

            /*
            BTree<int> bTree = new BTree<int>(3);
            bTree.Insert(10);
            bTree.Print();
            bTree.Insert(5);
            bTree.Print();
            bTree.Insert(15);
            bTree.Print();
            bTree.Insert(3);
            bTree.Print();
            bTree.Insert(7);
            bTree.Print();
            bTree.Insert(12);
            bTree.Print();
            bTree.Insert(17);
            bTree.Print();
            bTree.Insert(1);
            bTree.Print();
            bTree.Insert(4);
            bTree.Print();
            bTree.Insert(6);
            bTree.Print();
            bTree.Insert(9);
            bTree.Print();
            bTree.Insert(11);
            bTree.Print();
            bTree.Insert(14);
            bTree.Print();
            bTree.Insert(16);
            bTree.Print();
            bTree.Insert(19);
            bTree.Print();
            bTree.Insert(25);
            bTree.Print();
            */
        }
    }
}
