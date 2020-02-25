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

public enum class SomeColors
{ Red, Yellow, Blue };

int main()
{
    Coordinate_4D^ point = gcnew Coordinate_4D(1, 2, 3, 4);
    Console::WriteLine("Value Types    ----   System::ValueTypes");
    Console::WriteLine("x Co Ordinate : {0}\ny Co Ordinate : {1}\nx Co Ordinate : {2}", point->x, point->y, point->z);
    Console::WriteLine("Time Ordinate : {0} Sec", point->time);

    Console::ReadKey();
    return 0;
}

// Value types are a means to allow the user to create new types beyond the primitive types;
// all value types derive from System::ValueType.The value types can be stored on the stack,
// and can be assigned using the equal operator.