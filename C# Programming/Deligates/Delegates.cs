using System;
using FunctionalityEvents;

namespace FunctionalityDelegates
{

    class programe
    {
        public delegate int TestDelegate();                             // 1. Prototype the delegate. 
        public delegate void TestMultiCastDelegate();


        public TestDelegate Var1, Var2, Var3, Var4;                     // 2. Prototype the delegage typedef
        public TestMultiCastDelegate Del1, Del2, Del3, Del4;

        public programe()
        {
            // Unicast Delegates
            Var1 = TestMethod1;                                         // 3. Assign Method to the delegate.
            Var2 = TestMethod2;
            Var3 = TestInitiliaze;
            Var4 = TestInitiliaze;

            Del1 = TestMethod3; 
            Del2 = TestMethod4;
            Del3 = TestMethod5;

            // OR
            // var1 = new TestDelegate(TestMethod1);
            // var2 = new TestDelegate(TestMethod2);

            // Multicast Delegates
            Var3 = Var1 + Var2;
            Var4 = Var2 - Var1;
            Del4 = Del1 + Del2 + Del3;
        }

        private int TestMethod1() { return 10; }
        private int TestMethod2() { return 20; }
        private int TestInitiliaze() { return 0; }


        private void TestMethod3() { Console.WriteLine("Delegate Via Test Method 1"); }
        private void TestMethod4() { Console.WriteLine("Delegate Via Test Method 2"); }
        private void TestMethod5() { Console.WriteLine("Delegate Via Test Method 3"); }

        static int Main()
        {
            // Delegates Opertion
            programe obj = new programe();

            Console.WriteLine("Using 1st Delegate : {0}", obj.Var1()); // 4. Utilize the delegate typedef
            Console.WriteLine("Using 2nd Delegate : {0}", obj.Var2());

            Console.WriteLine("Execute Multicast Delegate 1 : {0}",obj.Var3());
            Console.WriteLine("Execute Multicast Delegate 2 : {0}",obj.Var4());
            Console.WriteLine("Execute Multicast Delegate 3 : ");
            obj.Del4();


            // Events Opertion
            Events ObjEvents = new Events();
            ObjEvents.CallAction();


            Console.ReadLine();
            return 0;
        }

        static void EventHandler(object caller, EventArgs arg)
        {
            
        }
    }
}


// If the delegate has a return type other than void and if the delegate is a multicast delegate, only 
// the value of the last invoked method will be returned. Along the same lines, if the delegate has an
// out parameter, the value of the output parameter, will be the value assigned by the last method.