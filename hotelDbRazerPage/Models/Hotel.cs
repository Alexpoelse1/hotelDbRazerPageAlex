namespace hotelDbRazerPage.Models
{
    public class Hotel
    {
        public int Hotel_No { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public Hotel()
        {
        }

        public Hotel(int hotel_No, string name, string address)
        {
            Hotel_No = hotel_No;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Hotel_No)}: {Hotel_No}, {nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
}
