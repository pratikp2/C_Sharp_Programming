using System;
using System.Collections.Generic;

namespace Array_List
{
    class Program
    {
        static void Main()
        {
            // 1.Array 
            var array1 = new int[5];            // One Dimentional 
            var array2 = new int[5, 2];         // Two Dimentional 
            var array3 = new int[5, 2, 3];      // Three Dimentional

            var array = new[] { 1, 2, 3, 4, 5 };


            // 2.Array Class (System.Array)
            // This is an abstract base class which means you cannot create an instance of this class. This class provides
            // a CreateInstance method for creating an instance of an array. The first parameter is the type. The second 
            // parameter is the dimension Once an array has been created, we can use the SetValue method to add items to 
            // an array

            // One Dimentional Array
            System.Array myArray1 = System.Array.CreateInstance(typeof(string),3);
            myArray1.SetValue("one", 0);
            myArray1.SetValue("two", 1);
            myArray1.SetValue("three", 2);

            Console.WriteLine();
            Console.WriteLine("myArray[{0}] = {1}", 0, myArray1.GetValue(0));
            Console.WriteLine("myArray[{0}] = {1}", 1, myArray1.GetValue(1));
            Console.WriteLine("myArray[{0}] = {1}", 2, myArray1.GetValue(2));

            // Two Dimentional Array
            System.Array myArray2 = System.Array.CreateInstance(typeof(string), 2, 2);
            myArray2.SetValue("one", 0,0);
            myArray2.SetValue("two", 0,1);
            myArray2.SetValue("three", 1,0);
            myArray2.SetValue("four", 1,1);

            Console.WriteLine();
            Console.WriteLine("myArray[{0}{1}] = {2}", 0,0, myArray2.GetValue(0,0));
            Console.WriteLine("myArray[{0}{1}] = {2}", 0,1, myArray2.GetValue(0,1));
            Console.WriteLine("myArray[{0}{1}] = {2}", 1,0, myArray2.GetValue(1,0));
            Console.WriteLine("myArray[{0}{1}] = {2}", 1,1, myArray2.GetValue(1,1));


            // List 
            List<int> nodes = new List<int>() { 1,2,3,4,5,6,7,8};
            nodes.Add(9);

            foreach (var num in nodes)
                Console.Write("{0} ",num);
            Console.WriteLine();
            Console.WriteLine(nodes.Count);

            Console.ReadLine();
        }
    }
}

// Ways to initiliaze 1.Array

// double[] balance = new double[10];
// double[] balance = { 2340.0, 4523.69, 3421.0 };
// int[] marks = new int[5] { 99, 98, 92, 97, 95 };
// int[] marks = new int[] { 99, 98, 92, 97, 95 };
