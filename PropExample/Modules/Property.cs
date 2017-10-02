using System;
namespace PropExample.Modules
{
    public class Property
    {
		public int Prop_Id { get; set; }
		public string Property_Desc { get; set; }
		public string Property_Type { get; set; }
		public string Property_Stat { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public int BedRoom { get; set; }
		public int BathRoom { get; set; }
		public int Garage { get; set; }
		public double Price { get; set; }

		public Property(string desc, string type, string stat, string addres, string city, int bed, int bath, int garage, double amount)
		{
			Property_Desc = desc;
			Property_Type = type;
			Property_Stat = stat;
			Address = addres;
			City = city;
			BedRoom = bed;
			BathRoom = bath;
			Garage = garage;
			Price = amount;

		}
        public Property()
        {
        }
    }
}
