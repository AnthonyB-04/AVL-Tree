using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    class AVLTree<T> : IEnumerable<T> where T : IComparable
    {

        internal protected class Node<T> where T : IComparable
        {
            public T Item { get; set; }
            public Node<T> Right { get; set; }
            public Node<T> Left { get; set; }
            public int Height { get; set; }

            public Node(T data)
            {
                Item = data;
                Height = 1;
            }

            public override string ToString()
            {
                return Item.ToString();
            }
        }


        public Node<T> root;
        private IEnumerator<T> enumerator;
        
        /*
        public AVLTree(SortedSet<T> set)
        {
            
            enumerator = set.GetEnumerator();
            
            for (int i = 0; i < set.Count; i++)
            {
                Node<T> node = new(set.ElementAt(i));
                Add(root, node);
            }

        }
        
        class TreeException : Exception
        {
            public TreeException(string message)
                : base(message) { }
        }

        private void Add(Node<T> root, Node<T> anotherNode)
        {

            if (root == null)
            {
                
                root = anotherNode;
            }
            else
            {
                try
                {
                    if (root.Item.CompareTo(anotherNode.Item) < 0)
                    {
                        if (root.Right != null)
                        {
                            Add(root.Right, anotherNode);
                        }
                        else
                        {
                            root.Right = anotherNode;
                        }
                        
                    }
                    else if (root.Item.CompareTo(anotherNode.Item) > 0)
                    {
                        if (root.Left != null)
                        {
                            Add(root.Left, anotherNode);
                        }
                        else
                        {
                            root.Left = anotherNode;
                        }
                        
                    }
                    
                }
                catch (TreeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        */

        public int GetHeight(Node<T> N)
        {
            if (N == null)
                return 0;
            return N.Height;
        }
        
        Node<T> RightRotate(Node<T> y)
        {
            Node<T> x = y.Left;
            Node<T> T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            y.Height = new[] { GetHeight(y.Left), GetHeight(y.Right) }.Max() + 1;
            x.Height = new[] { GetHeight(x.Left), GetHeight(x.Right) }.Max() + 1;
            return x;
        }

        Node<T> LeftRotate(Node<T> x)
        {
            Node<T> y = x.Right;
            Node<T> T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            x.Height = new[] { GetHeight(x.Left), GetHeight(x.Right) }.Max() + 1;
            y.Height = new[] { GetHeight(y.Left), GetHeight(y.Right) }.Max() + 1;
            return y;
        }

        // Повертає коефіцієнт балансу вузла
        int GetBalanceFactor(Node<T> N)
        {
            if (N == null)
                return 0;
            return GetHeight(N.Left) - GetHeight(N.Right);
        }

        public Node<T> InsertNode(Node<T> node, T item)
        {

            // Знаходимо позицію для вставки вузла
            if (node == null)
                return (new Node<T>(item));
            if (item.CompareTo(node.Item) < 0)
                node.Left = InsertNode(node.Left, item);
            else if (item.CompareTo(node.Item) > 0)
                node.Right = InsertNode(node.Right, item);
            else
                return node;

            // Оновлюємо коефіцієнт балансу кожного вузла і збалансовуємо дерево
            node.Height = 1 + new[] { GetHeight(node.Left), GetHeight(node.Right) }.Max();
            int balanceFactor = GetBalanceFactor(node);
            if (balanceFactor > 1)
            {
                if (item.CompareTo(node.Left.Item) < 0)
                {
                    return RightRotate(node);
                }
                else if (item.CompareTo(node.Left.Item) > 0)
                {
                    node.Left = LeftRotate(node.Left);
                    return RightRotate(node);
                }
            }
            if (balanceFactor < -1)
            {
                if (item.CompareTo(node.Right.Item) > 0)
                {
                    return LeftRotate(node);
                }
                else if (item.CompareTo(node.Right.Item) < 0)
                {
                    node.Right = RightRotate(node.Right);
                    return LeftRotate(node);
                }
            }
            return node;
        }

        Node<T> MimumValueNode(Node<T> node)
        {
            Node<T> current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        public Node<T> DeleteNode(Node<T> root, T item)
        {

            // Знайдемо вузол, який потрібно видалити, і видаляємо його
            if (root == null)
                return root;
            if (item.CompareTo(root.Item) < 0)
                root.Left = DeleteNode(root.Left, item);
            else if (item.CompareTo(root.Item) > 0)
                root.Right = DeleteNode(root.Right, item);
            else
            {
                if ((root.Left == null) || (root.Right == null))
                {
                    Node<T> temp = null;
                    if (temp == root.Left)
                        temp = root.Right;
                    else
                        temp = root.Left;
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else
                        root = temp;
                }
                else
                {
                    Node<T> temp = MimumValueNode(root.Right);
                    root.Item = temp.Item;
                    root.Right = DeleteNode(root.Right, temp.Item);
                }
            }
            if (root == null)
                return root;

            // Оновлюємо коефіцієнт балансу кожного вузла та збалансовуємо дерево
            root.Height = new[] { GetHeight(root.Left), GetHeight(root.Right) }.Max() + 1;
            int balanceFactor = GetBalanceFactor(root);
            if (balanceFactor > 1)
            {
                if (GetBalanceFactor(root.Left) >= 0)
                {
                    return RightRotate(root);
                }
                else
                {
                    root.Left = LeftRotate(root.Left);
                    return RightRotate(root);
                }
            }
            if (balanceFactor < -1)
            {
                if (GetBalanceFactor(root.Right) <= 0)
                {
                    return LeftRotate(root);
                }
                else
                {
                    root.Right = RightRotate(root.Right);
                    return LeftRotate(root);
                }
            }
            return root;
        }

        void PreOrder(Node<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Item + " ");
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        public void PrintTree(Node<T> currPtr, string indent, bool last)
        {
            if (currPtr != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("R--");
                    indent += "   ";
                }
                else
                {
                    Console.Write("L--");
                    indent += "|  ";
                }
                Console.Write(currPtr.Item);
                PrintTree(currPtr.Left, indent, false);
                PrintTree(currPtr.Right, indent, true);
            }
            
        }

        public IEnumerator<T> GetEnumerator()
        {
            
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            
            return (IEnumerator)GetEnumerator();
        }

    }
}
