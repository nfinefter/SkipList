using System;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    class Node<T>
    {
        public T Value;

        public int Height { get; set; }

        public Node<T> Next;
        public Node<T> Down;

        public Node(T value, int height)
        {
            Value = value;
            Height = height;
        }

    
    }
}
