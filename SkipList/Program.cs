using System;
using System.Linq;

namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            list.Insert(5);
            list.Insert(6);
            list.Insert(3);
            list.Insert(3);
            list.Delete(3);
            list.Insert(2);
            list.Insert(4);
            list.Insert(9);

            foreach (int num in list)
            {
                Console.WriteLine(num);
            }

            //int[] arr = new int[20];
            //list.CopyTo(arr, 0);

            //foreach (int val in arr)
            //{
            //    Console.WriteLine(val);
            //}

            Console.WriteLine(list.ToString());
        }
    }
}
