using System;

namespace ClassLibraryTree
{
    public class OptimalBSTNode
    {
        public int key;
        public double probability;
        public OptimalBSTNode left;
        public OptimalBSTNode right;
        public OptimalBSTNode(int key, double probability)
        {
            this.key = key;
            this.probability = probability;
            this.left = null;
            this.right = null;
        }
    }

    public class OptimalBST
    {
        public static OptimalBSTNode BuildOptimalBST(int[] keys, double[] probabilities)
        {
            int n = keys.Length;
            double[,] cost = new double[n, n];
            OptimalBSTNode[,] root = new OptimalBSTNode[n, n];
            for (int i = 0; i < n; i++)
            {
                cost[i, i] = probabilities[i];
                root[i, i] = new OptimalBSTNode(keys[i], probabilities[i]);
            }
            for (int len = 2; len <= n; len++)
            {
                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len - 1;
                    cost[i, j] = double.PositiveInfinity;
                    for (int k = i; k <= j; k++)
                    {
                        double leftCost = (k > i) ? cost[i, k - 1] : 0;
                        double rightCost = (k < j) ? cost[k + 1, j] : 0;
                        double totalCost = leftCost + rightCost + Sum(probabilities, i, j);
                        if (totalCost < cost[i, j])
                        {
                            cost[i, j] = totalCost;
                            root[i, j] = new OptimalBSTNode(keys[k], probabilities[k]);
                            root[i, j].left = (k > i) ? root[i, k - 1] : null;
                            root[i, j].right = (k < j) ? root[k + 1, j] : null;
                        }
                    }
                }
            }
            return root[0, n - 1];
        }

        private static double Sum(double[] probabilities, int i, int j)
        {
            double sum = 0;
            for (int k = i; k <= j; k++)
            {
                sum += probabilities[k];
            }
            return sum;
        }

        public static void InOrderTraversal(OptimalBSTNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.left);
                Console.Write(node.key + " ");
                InOrderTraversal(node.right);
            }
        }
    }
}
