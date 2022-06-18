using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    class List<T> : IEnumerable<T> where T : IComparable<T>
    {
        public int Count;

        public Node<T> Head;

      

        public List()
        {
            Head = new Node<T>(default, 1);
        }

        private Node<T> CreateColumn(T value, int height)
        {
            Node<T> newNode = new Node<T>(value, height);
            Node<T> curr = newNode;

            for (int i = 0; i < height - 1; i++)
            {
                curr.Down = new Node<T>(value, curr.Height - 1);
                curr = curr.Down;
            }

            return newNode;
        }

        //public Node<T> CreateLayers(T value)
        //{
        //    Node<T> newNode = new Node<T>(value);
        //    newNode.Height = ChooseRandomHeight();

        //    return newNode;
        //}
            // Creates a linked list of nodes with point down of height


        //fix head to make it a linked list
        public void Insert(T value)
        {
            Node<T> temp = Head;
            Node<T> newNode = CreateColumn(value, ChooseRandomHeight());

            while (temp.Height != newNode.Height)
            {
                while (temp.Next != null && value.CompareTo(temp.Next.Value) > 0)
                {
                    temp = temp.Next;
                }
                temp = temp.Down;
            }

            newNode.Next = temp.Next;
            temp.Next = newNode;

            while (temp.Height > 1)
            {
                temp = temp.Down;
                newNode = newNode.Down;
                while (temp.Next != null && value.CompareTo(temp.Next.Value) > 0)
                {
                    temp = temp.Next;
                }
                newNode.Next = temp.Next;
                temp.Next = newNode;
            }

         
        }

        public void Remove(T value)
        {
            Node<T> curr = Head;
            
            while(curr.Down != null)
            {
                if (curr.Value.CompareTo(value) > 0)
                {
                    curr = curr.Down;
                    curr.Height--;
                }
                if (curr.Value.CompareTo(value) > 0)
                {
                    curr = curr.Next;
                }
                if (curr.Value.CompareTo(value) == 0)
                {
                    curr.Height--;
                }
            }
            if (curr.Height == -1)
            {
                return;
            }
        }


        public int ChooseRandomHeight()
        {
            int Height = 1;
            Random rand = new Random();

            while (rand.Next(1, 3) == 1 && Height <= Head.Height + 1)
            {
                Height++;
                while (Height > Head.Height)
                {
                    Head.Height++;
                }
                
            }
        
            return Height;
        }

        public IEnumerator<T> GetEnumerator()
        {

            Node<T> curr = Head.Next;

            while (curr.Down != null)
            {
                
                curr = curr.Down;
            }
            while (curr.Next != null)
            {
                curr = curr.Next;
                yield return curr.Value;
                
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
