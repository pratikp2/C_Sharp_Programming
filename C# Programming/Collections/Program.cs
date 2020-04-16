using System;
using System.Collections.Generic;

namespace CollectionsData
{
    class Program
    {
        static void Main()
        {
            // 1. List (Size incerease with multiplication factor of 2 at Each resize )

            List<int> list = new List<int>() { 1,2,3,4,5,6,7,8};
            list.Add(9);

            Console.WriteLine("List : ");
            foreach (var num1 in list)
                Console.Write("{0} ",num1);
            Console.WriteLine();
            Console.WriteLine(list.Count);
            Console.WriteLine("{0}\n",list.Capacity);


            // 2. Queue
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            Console.WriteLine("Queue : ");
            while (queue.Count > 0)
                Console.Write("{0} ",queue.Dequeue());
            Console.WriteLine("\n{0}\n", queue.Count);


            // 3. Stack
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Console.WriteLine("Stack : ");
            while (stack.Count > 0)
                Console.Write("{0} ", stack.Pop());
            Console.WriteLine("\n{0}\n", queue.Count);


            // 4. HashSet (Stores all unique Elements)
            HashSet<int> set1 = new HashSet<int>() { 2, 3, 1, 9, 4 };
            HashSet<int> set2 = new HashSet<int>() { 8, 3, 2, 9, 5 };
            HashSet<int> set3 = new HashSet<int>() { 2, 3, 1, 9, 4 };
            HashSet<int> set4 = new HashSet<int>() { 2, 3, 1, 9, 4 };
            HashSet<int> set5 = new HashSet<int>() { 2, 3, 1, 9, 4 };
            set1.Add(6);

            Console.WriteLine("HashSet : ");
            foreach (var num2 in set1)
                Console.Write("{0} ", num2);

            set1.IntersectWith(set2);
            Console.Write("\nIntersecion of set1 and set2           : ");
            foreach (var num3 in set1)
                Console.Write("{0} ", num3);

            set3.SetEquals(set2);
            Console.Write("\nSetting Equal to set2                  : ");
            foreach (var num4 in set3)
                Console.Write("{0} ", num4);

            set4.UnionWith(set2);
            Console.Write("\nUnion of set1 and set2                 : ");
            foreach (var num5 in set4)
                Console.Write("{0} ", num5);

            set5.SymmetricExceptWith(set2);
            Console.Write("\nSymmetricExceptWith of set1 and set2   : ");
            foreach (var num6 in set5)
                Console.Write("{0} ", num6);


            // 5. LinkList (Doublly Linked List)
            LinkedList<int> linklist = new LinkedList<int>();
            linklist.AddFirst(1);
            linklist.AddLast(2);
            linklist.AddBefore(linklist.First, 3);
            linklist.AddAfter(linklist.Last, 4);
            linklist.AddLast(5);

            Console.WriteLine("\n\nLinked List : ");
            var node1 = linklist.First;
            while (node1 != null)
            {
                Console.Write("{0} ", node1.Value);
                node1 = node1.Next;
            }
            Console.WriteLine();

            var node2 = linklist.Last;
            while (node2 != null)
            {
                Console.Write("{0} ", node2.Value);
                node2 = node2.Previous;
            }


            // 6. Dictionary
            Dictionary<int,string> map = new Dictionary<int,string>();
            map.Add(1,"One");
            map.Add(2,"Two");
            map.Add(3,"Three");
            map.Add(4,"Four");
            map.Add(5,"Five");

            Console.WriteLine("\n\nDictionary / Map : ");
            foreach (var num7 in map)
                Console.WriteLine("{0} : {1} ", num7.Key,num7.Value);


            var sortedList = new SortedList<int, string>();
            var sortedset = new SortedSet<int>();
            var sortedDictionart = new SortedDictionary<int,string>();
            
            // Sorted Colletions store data int sorted manner
            // Sorted Dictionary and Sorted Lists are almost same with one key difference.
            // Sorted Dictionary is optimised for efficient and quick insert and removal user more memory.
            // Sorted Lists are optimised for quick iterations used less memory.

            Console.ReadLine();
        }
    }
}

