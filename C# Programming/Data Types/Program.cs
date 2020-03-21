using System;

namespace Data_Types
{
    class Program
    {
        static void Main()
        {
            // Value Type : Integral Type
            Console.WriteLine("Size of int      :   {0}", sizeof(int));         //  -2147483648 to 2147483647
            Console.WriteLine("Size of uint     :   {0}", sizeof(uint));        //  0 to 4294967295
            Console.WriteLine("Size of UInt16   :   {0}", sizeof(UInt16));      //
            Console.WriteLine("Size of UInt32   :   {0}", sizeof(UInt32));      //
            Console.WriteLine("Size of UInt64   :   {0}", sizeof(UInt64));      //
            Console.WriteLine("Size of byte     :   {0}", sizeof(byte));        //  0 to 255
            Console.WriteLine("Size of sbyte    :   {0}", sizeof(sbyte));       //  -128 to 127
            Console.WriteLine("Size of long     :   {0}", sizeof(long));        //  -9223372036854775808 to 9223372036854775807
            Console.WriteLine("Size of ulong    :   {0}", sizeof(ulong));       //  0 to 18446744073709551615
            Console.WriteLine("Size of short    :   {0}", sizeof(short));       //  -32768 to 32767
            Console.WriteLine("Size of ushort   :   {0}", sizeof(ushort));      //  0 to 65535
            Console.WriteLine("Size of char     :   {0}", sizeof(char));        //  U +0000 to U +ffff

            // Value Type : Integral Type
            Console.WriteLine("Size of double   :   {0}", sizeof(double));      //  (+/-)5.0 x 10^-324 to (+/-)1.7 x 10^308
            Console.WriteLine("Size of float    :   {0}", sizeof(float));       //  -3.4 x 10^38 to + 3.4 x 10^38

            // Value Type : Decimal and Boolean
            Console.WriteLine("Size of decimal  :   {0}", sizeof(decimal));     //  (-7.9 x 10^28 to 7.9 x 10^28) / 10^0 to 28
            Console.WriteLine("Size of bool     :   {0}", sizeof(bool));        //  True or False


            // Object Type
            // alias for System.Object class
            // value conversion into object - boxing, object conversion into value - unboxing 

            object var = 10;


            // Dynamic Type
            // Type checking for these types of variables takes place at run-time.

            dynamic d = 10;

            // Nullable Values
            // C# provides a special data types, the nullable types, to which you can assign normal range of values as well as null values.
            int? num1 = null;
            num1 = 45;


            // The Null Coalescing Operator(??)
            // The null coalescing operator is used with the nullable value types and reference types.It is used for converting
            // an operand to the type of another nullable(or not) value type operand, where an implicit conversion is possible
            // If the value of the first operand is null, then the operator returns the value of the second operand, otherwise
            // it returns the value of the first operand.

            double? num2 = null;
            double? num3 = 3.14157;
            double num4;

            num4 = num2 ?? 5.34;    // num4 = 5.34
            num4 = num3 ?? 5.34;    // num4 = 3.14157 

            Console.ReadLine();
        }
    }
}

