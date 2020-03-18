	#include "pch.h"
	#using <mscorlib.dll>
	using namespace System;

//ref class means it's a .Net class.
//Class resides in managed or .Net memory
public ref class SplitName
{
private:
	System::String^ FName, ^ LName;

public:
	SplitName(System::String^ FullName)
	{
		int pos = FullName->IndexOf(" ");
		if (pos < 0)
			throw gcnew System::Exception("Invalid full name!");
		FName = FullName->Substring(0, pos);
		LName = FullName->Substring(pos + 1, FullName->Length - pos - 1);
	}

	void print() { Console::WriteLine("First name: {0}\nLastName: {1}", FName, LName); }
};

int main()
{
	// local copy
	SplitName s("Jhon Doe");
	s.print();

	// managed heap
	//SplitName *s = new SplitName("Jane Doe");       //Not Allowed Because its a .Net class
	SplitName^ ms = gcnew SplitName("Jane Doe");
	ms->print();

	Console::ReadLine();
	return 0;
}

//In C++, we have the "*" to denote a pointer, and in C++ / CLI, we have the ^ to denote a handle.
//Now, a "*" designates a native pointer which resides on the CRT heap, whereas the handles designate
//"safe pointers" and reside on the managed heap.

//The handles can be thought of as references, and unlike native pointers, they won't cause memory 
//leaks if they are not properly deleted, since the GC will take care of that, and they don't have
//a fixed memory address and thus will be moved around during execution.

//To create a new reference of a certain class or value type, we have to allocate it with the "gcnew" keyword.
// It careates the object of the class in .Net memory and no need to free it it will automatically freed at the
// time of garbage collection.

//http://www.functionx.com/cppcli/index.htm for detailed study of C++ CLR.