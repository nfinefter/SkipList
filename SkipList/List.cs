using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    class List<T> : IEnumerable<T> , ICollection<T>  where T : IComparable<T>
    {
        public int Count;

        public Node<T> Head;

        int ICollection<T>.Count => Count;

        public bool IsReadOnly => false;

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

            Node<T> newNode = CreateColumn(value, ChooseRandomHeight());
            Node<T> temp = Head;

            while (temp.Height != newNode.Height)
            {
                while (temp.Next != null && value.CompareTo(temp.Next.Value) > 0)
                {
                    temp = temp.Next;
                }
                temp = temp.Down;
            }

            //newNode.Next = temp.Next;
            //temp.Next = newNode;

            //while (temp.Height > 1) // this should be while temp.Height > 0
            while (temp != null)
            {
                //while (temp.Next != null && value.CompareTo(temp.Next.Value) > 0)
                if (CompareToNext(value, temp) > 0)
                {
                    temp = temp.Next;
                }
                
               

                //while (temp.Down != null && value.CompareTo(temp.Next.Value) <= 0)
                else if (CompareToNext(value, temp) <= 0)
                {
                    ConnectNodes(temp, newNode, temp.Next);
                    
                    //newNode.Next = temp.Next;
                    //temp.Next = newNode;

                    temp = temp.Down;
                    newNode = newNode.Down;
                }
              
                
            }

         
        }

        private int CompareToNext(T value, Node<T> temp)
        {
            if (temp.Next == null)
            {
                return -1;
            }

            return value.CompareTo(temp.Next.Value);
        }

        private void ConnectNodes(Node<T> prev, Node<T> node, Node<T> next)
        {
            node.Next = next;
            prev.Next = node;
        }
        private void ConnectNodes(Node<T> prev, Node<T> next)
        {
            prev.Next = next;            
        }
        public bool Delete(T value)
        {
            Node<T> temp = Head;
            bool Deleted = false;

            while(temp != null)
            {
                if (CompareToNext(value, temp) > 0)
                {
                    temp = temp.Next;
                }



                //while (temp.Down != null && value.CompareTo(temp.Next.Value) <= 0)
                else if (CompareToNext(value, temp) < 0)
                {
                    temp = temp.Down;
                   
                    
                }
                else if (CompareToNext(value, temp) == 0)
                {
                    ConnectNodes(temp, temp.Next.Next);
                    Deleted = true;
                }
                
            }
            return Deleted;
          
        }
        public bool Contains(T value)
        {
            Node<T> temp = Head;

            while (temp != null)
            {
                if (CompareToNext(value, temp) > 0)
                {
                    temp = temp.Next;
                }



                //while (temp.Down != null && value.CompareTo(temp.Next.Value) <= 0)
                else if (CompareToNext(value, temp) < 0)
                {
                    temp = temp.Down;


                }
                else if (CompareToNext(value, temp) == 0)
                {
                    return true;
                }
            }
            return false;
        }


        public int ChooseRandomHeight()
        {
            int Height = 1;
            Random rand = new Random();

            while (rand.Next(1, 3) == 1 && Height <= Head.Height + 1)
            {
                Height++;
                while (Height > Head.Height) // Height = 1, Height > 2     4 -> 4 -> 1
                {
                    Node<T> temp = new Node<T>(default, Height);
                    temp.Down = Head;
                    Head = temp;
                }
                
            }
        
            return Height;
        }

        public IEnumerator<T> GetEnumerator()
        {

            Node<T> curr = Head;

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
        public override string ToString()
        {
            int height = Head.Height;
            string skipList = "";
            Node<T> temp = Head;
            Node<T> curr = Head;
            while (height > 0)
            {
                
                while (temp != null)
                {
                    skipList += temp.Value;
                    temp = temp.Next;
                }
                skipList += "\n";
                curr = curr.Down;
                temp = curr;
                height--;
            }

            return skipList;
        }

        public void Add(T item)
        {
            Insert(item);
        }

        public void Clear()
        {
            Head = new Node<T>(default, 0);
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T item in this)
            {
                array[arrayIndex++] = item;
            }

            //for (; arrayIndex < array.Length; arrayIndex++)
            //{
            //    array[arrayIndex] = (T)GetEnumerator();
            //}
        }

        bool ICollection<T>.Remove(T item)
        {
            return Delete(item);
        }
    }
}
