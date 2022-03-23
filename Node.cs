using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    internal class Node<T> where T : IComparable
    {
        public T Value { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Left { get; set; }
        public Node(T data)
        {
            Value = data;
        }

        public Node(T data, Node<T> left, Node<T> right)
        {
            Value = data;
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
