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
            Head = new Node<T>(default);
            Head.Height = 1;
        }

        public Node<T> CreateLayers(T value)
        {
            Node<T> newNode = new Node<T>(value);
            newNode.Height = ChooseRandomHeight();

            return newNode;
        }
            // Creates a linked list of nodes with point down of height

        public void Insert(T value)
        {
            Node<T> temp = Head;
            Node<T> newNode = CreateLayers(value);

            while (temp.Height != newNode.Height && temp.Down != null)
            {
                temp = temp.Down;
            }

            ConnectNodes(temp, newNode, temp.Next);

            //addd the compare tos right under
            while(temp.Value.CompareTo(temp.Down.Value) > 0)
            {
                temp = temp.Down;
                while(temp.Next != null)
                {
                    temp = temp.Next;
                }
            }

            if (temp.Height == -1)
            {
                return;
            }
        }
        private void ConnectNodes(Node<T> Down, Node<T> newNode, Node<T> next)
        {
            newNode.Next = next;
            newNode.Down = Down;

            newNode.Down.Next = newNode; 

            if (newNode.Next != null)
            {
                newNode.Next.Down = newNode;

            }

        }

        public void Remove(T value)
        {
            Node<T> curr = Head;
            
            while(curr.Down != null)
            {

            }
            if (curr.Height == -1)
            {
                return;
            }
        }


        public int ChooseRandomHeight()
        {
            int Height = 0;
            Random rand = new Random();

            while (rand.Next(0, 2) == 1 && Height <= Head.Height + 1)
            {
                while (Height > Head.Height)
                {
                    Head.Height++;
                }
                Height++;
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
