using System;
namespace PropExample.Modules
{
    public class Customer
    {
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Contact { get; set; }
		public string Gender { get; set; }


		public Customer(string firstname, string lastname, string email, string password)
		{
			Firstname = firstname;
			Lastname = lastname;
			Email = email;
			Password = password;

		}


		public Customer(string emails, string passwords)
		{
			Email = emails;
			Password = passwords;
		}
        public Customer()
        {
        }
    }
}
