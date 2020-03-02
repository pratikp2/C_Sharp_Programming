#include "pch.h"
using namespace System;

ref class Test
{
public:
    int m_var;
};

void CreateDataInstances(int count)
{
    for (int i = 0; i < count; i++)
        gcnew Test();
}

void NativeFunc(int* ptr) { *ptr = *ptr + 100; }

int main()
{
    Test^ t1 = gcnew Test();
    CreateDataInstances(1000);
    Test^ t2 = gcnew Test();

    pin_ptr<int> p1 = &t1->m_var;
    interior_ptr<int> p2 = &t2->m_var;

    NativeFunc(p1);
    Console::WriteLine(t1->m_var);

    //Console::WriteLine(p1);
    //printf("pinned p1 = %p : interior ptr p2 = %p\r\n", p1, p2);
    //printf("Address for Pinning Pointer P1 %p", p1);
    //printf("Address for Pinning Pointer P1 %p", p1);

    //cout << "Address for Pinning Pointer P1 : " << /*p1 <<*/ endl;
    return 0;
}
