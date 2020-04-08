using System;

//Runtime or Dynamic Polymorphism

namespace Runtime_Polymorphism
{
    public interface Parent1
    {
        void Method1();     // No ned to mention Access specifier as all the methods will bu public.
        void Method2();
    }

    public abstract class Parent2
    {
        public abstract void Method1();
        public virtual void Method2() { Console.WriteLine("Called Parent2 Method2"); }
    }

    public class Parent3
    {
        public void Method1() { Console.WriteLine("Called Parent3 Method1"); }
        public virtual void Method3() { Console.WriteLine("Called Parent3 Method3"); }
    }

    class Child : Parent3, Parent1
    {
        public void Method1() { Console.WriteLine("Called Child Method1"); }
        public void Method2() { Console.WriteLine("Called Child Method2"); }
        public override void Method3() { Console.WriteLine("Called Child Method3"); }
    }
    class TestCode
    {
        static void Main()
        {
            Parent1 obj1 = new Child();
            obj1.Method1();
            obj1.Method2();

            Parent3 obj2 = new Child();
            obj2.Method1();
            obj2.Method3();
        }
    }
}
