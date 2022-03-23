using System;
using System.Collections;
using System.Collections.Generic;

namespace AVL_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var set = new SortedSet<int>()
            {
                33, 13, 53, 9, 21, 61, 8, 11
            };

            var tree = new AVLTree<int>(set);
            */

            var tree = new AVLTree<int>();
            tree.root = tree.InsertNode(tree.root, 33);
            tree.root = tree.InsertNode(tree.root, 13);
            tree.root = tree.InsertNode(tree.root, 53);
            tree.root = tree.InsertNode(tree.root, 9);
            tree.root = tree.InsertNode(tree.root, 21);
            tree.root = tree.InsertNode(tree.root, 61);
            tree.root = tree.InsertNode(tree.root, 8);
            tree.root = tree.InsertNode(tree.root, 11);

            tree.PrintTree(tree.root, "", true);
            Console.WriteLine("");

            tree.root = tree.DeleteNode(tree.root, 13);
            Console.WriteLine("After Deletion: ");
            tree.PrintTree(tree.root, "", true);
            Console.WriteLine("");

            Console.ReadKey();
        }
            
    }
}
