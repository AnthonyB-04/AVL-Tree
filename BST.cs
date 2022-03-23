using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    internal class BST<T> : IEnumerable<T> where T : IComparable
    {

        private Node<T> _root;
        protected bool isReversedReading = false;
        private IEnumerator<T> _enumerator;
        private int indexOfNode = -1;

        /*
        public BST()
        {
            _root = null;
        }
        */

        public BST(bool order, SortedSet<T> set)
        {
            isReversedReading = order;
            _enumerator = set.GetEnumerator();
            indexOfNode = set.Count - 1;

            if (isReversedReading)
            {
                for (int i = indexOfNode; i > 0; i--)
                {
                    Node<T> node = new(set.ElementAt(i));
                    Add(_root, node);
                }
            }
            else
            {
                for (int i = 0; i < set.Count; i++)
                {
                    Node<T> node = new(set.ElementAt(i));
                    Add(_root, node);
                }
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
                indexOfNode = 0;
                root = anotherNode;
            }
            else
            {
                try
                {
                    if (root.Value.CompareTo(anotherNode.Value) < 0)
                    {
                        if (root.Right != null)
                        {
                            Add(root.Right, anotherNode);
                        }
                        else
                        {
                            root.Right = anotherNode;
                        }

                        indexOfNode++;
                    }
                    else if (root.Value.CompareTo(anotherNode.Value) > 0)
                    {
                        if (root.Left != null)
                        {
                            Add(root.Left, anotherNode);
                        }
                        else
                        {
                            root.Left = anotherNode;
                        }

                        indexOfNode++;
                    }
                    else
                    {
                        throw new Exception("Tree Exception: root == anotnerNode");
                    }
                }
                catch (TreeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int GetIndex()
        {
            return indexOfNode;
        }

    }
}
