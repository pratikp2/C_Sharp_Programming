using System;


namespace Properties
{
    class program
    {
        private int var1;
        public int HandleVar1        // 1. Proprty : Ability to add logic in Getters and Setters
        {
            get 
            {
                Console.WriteLine("Getter Called \n");
                return var1;
            }
            set 
            {
                var1 = value;
                Console.WriteLine("Setter Called");
                // Value is an explicit argument which is passed as argument to set.
            }
        }

        public int HandleVar2 { get; set; }     // 2. Property : Getters ans setters, can read and write values automatically

        public int HandleVar3                   // 3. Property : Can Modify the Access of the Setter.
        {
            get;
            private set;
        }

        static int Main()
        {
            program obj = new program();

            obj.HandleVar1 = 10;
            Console.WriteLine("{0}\n",obj.HandleVar1);

            obj.HandleVar2 = 20;
            Console.WriteLine("{0}\n", obj.HandleVar2);

            obj.HandleVar3 = 30;
            Console.WriteLine("{0}\n", obj.HandleVar3);

            Console.ReadLine();
            return 0;
        }
    }
}
