using System;

namespace Access_Specifiers
{
    class Parent
    {
        private string privateVariable; 
        internal string internalVariable;
        protected string protectedVariable;
        public string publicVariable;
        protected internal string protectedInternalVariable;

        private string PrivateFunction(){ return privateVariable;}
        internal string InternalFunction(){ return internalVariable;}
        protected string ProtectedFunction(){ return protectedVariable;}
        public string PublicFunction(){ return publicVariable;}
        protected internal string ProtectedInternalFunction(){ return protectedInternalVariable;}

        static void Main()
        {
            Console.ReadKey();
        }
    }

    class Child : Parent
    {

    }
}

// Types of Access Specifiers

// 1. public :
// Public access specifier allows a class to expose its member variables and member functions to other 
// functions and objects. Any public member can be accessed from outside the class.

// 2. protected
// Protected access specifier allows a child class to access the member variables and member functions
// of its base class. This way it helps in implementing inheritance.We will discuss this in more details
// in the inheritance chapter.

// 3. private :
// Private access specifier allows a class to hide its member variables and member functions from other
// functions and objects.Only functions of the same class can access its private members.Even an instance
// of a class cannot access its private members.

// 4. internal
// internal plays an important role when you want your class members to be accessible within the assembly.
// An assembly is the produced .dll or .exe from your.NET Language code (C#). Hence, if you have a C# project
// that has ClassA, ClassB and ClassC then any internal type and members will become accessible across the 
// classes with in the assembly.

// 5. protected internal
// Is a combination of protected and internal both. A protected internal will be accessible within the assembly
// due to its internal flavor and also via inheritance due to its protected flavor.