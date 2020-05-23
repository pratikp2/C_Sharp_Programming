enum Race
{
    Earthling,
    Marsian,
    Kerptoniyan
}

namespace PointAndLines
{
    class User
    {
        public static int currentUsers;
        public readonly int ID;             // Read only    : Value has to be assigned at time of execution.
        public const int HEIGHT = 150;      // const        : Value must be assigned before execution in compile time. 
        public Race race;

        private string username;            //  Fields    
        private int password;


        public string Username              // Property
        {
            get { return "The Username is " + username; }

            //set                          setting only getter is making property read only
            //{
            //    if (value.Length > 3 && value.Length < 10) { username = value; }
            //    else
            //    {
            //        System.Console.WriteLine("Oops..! Entered username is invalid. Please enter new Username with letters more than 4 and less than 10");
            //    }
            
            //}
        }
        public int Password
        {
            //get { return password; }      Setting only setter making property write only

            set
            {
                if (value > 3 && value < 10) { password = value; }
                else
                {
                    System.Console.WriteLine("Oops..! Entered password is invalid. Please enter new password between 4 to 10");
                }
            }
        }

        public User()
        {
            currentUsers++;
            ID = currentUsers;
        }
        public User(string username, Race race)
        {
            this.username = username;
            currentUsers++;
            ID = currentUsers;
            this.race = race;
        }
    }
}