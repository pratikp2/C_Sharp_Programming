namespace PointAndLines
{
    class User
    {
        private string username;           //  Fields    
        private int password;

        public string Username             // Property
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
        public User() { }
        public User(string username, int password)
        {
            this.username = username;
            this.password = password;
        }
    }
}