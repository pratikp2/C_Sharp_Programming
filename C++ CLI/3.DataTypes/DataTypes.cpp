#include "pch.h"
#using <mscorlib.dll>
using namespace System;

public value struct Coordinate_4D
{
    int x, y, z, time;
    Coordinate_4D(int x, int y, int z, int t)
    {
        this->x = x;
        this->y = y;
        this->z = z;
        this->time = t;
    }
};

public enum class Colors
{ Red, Yellow, Blue };

int main()
{
    // Value Types :
    Coordinate_4D^ point = gcnew Coordinate_4D(1, 2, 3, 4);
    Console::WriteLine("Value Types    ----   System::ValueTypes");
    Console::WriteLine("x Co Ordinate : {0}\ny Co Ordinate : {1}\nx Co Ordinate : {2}", point->x, point->y, point->z);
    Console::WriteLine("Time Ordinate : {0} Sec", point->time);

    // Enums :
    Colors ^ clrs1 = Colors::Blue;
    Colors ^ clrs2 = Colors::Red;
    Console::WriteLine("\nSelected 1st Color : {0}", clrs1);
    Console::WriteLine("Selected 2st Color : {0}\n", clrs2);

    // Array :
    cli::array<int> ^ arr1 = gcnew cli::array<int> {1, 2, 3};        // Creating Array of Three Integers
    cli::array<int> ^ arr2 = gcnew cli::array<int>(100) { 4, 5, 5};  // Creating array of 100 elements and Initiliazing 1st three.
    cli::array<int, 3> ^ arr3 = gcnew cli::array<int, 3>(4, 5, 2);   // Creating 3 Dimentional Array of Size 4 * 5 * 2 

    for each (int v in arr1)
        Console::WriteLine("{0}", v);
    Console::WriteLine("\n");

    // Array Of Classes :
    cli::array<String^> ^ strs = gcnew cli::array<String^> {"Hello", "World"};

    for each (String ^ v in strs)
        Console::WriteLine("{0}", v);
    Console::WriteLine("\n");

    Console::ReadKey();
    return 0;
}

// Value types are a means to allow the user to create new types beyond the primitive types;
// all value types derive from System::ValueType.The value types can be stored on the stack,
// and can be assigned using the equal operator.

//http://www.functionx.com/cppcli/index.htm for detailed study of C++ CLR.