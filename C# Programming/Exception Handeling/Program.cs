using System;

namespace Exception_Handeling
{
    class Program : Exception
    {
        static void Main()
        {
            try
            {
                CheckArgument();
                CheckFormat();
            }

            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                // throw;                      
                // rethrow : someone has to catch the exception again or Code will crash. 
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Console.WriteLine("Includes the code which is needed to be executed after ant exception is thrown.");
                Console.ReadLine();
            }

        }

        static void CheckArgument()
        {
            throw new ArgumentException($"Invalid Agrument");
        }

        static void CheckFormat()
        {
            throw new ArgumentException($"Invalid Format");
        }

        static void CheckCoustom()
        {
            throw new Exception($"Invalid Format");
        }
    }

}
