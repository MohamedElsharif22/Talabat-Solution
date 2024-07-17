namespace Talabat.Core.Entities.Order
{
    public class BuyerAddress
    {
        public BuyerAddress()
        {

        }
        public BuyerAddress(string fName, string lName, string city, string street, string countery)
        {
            FName = fName;
            LName = lName;
            City = city;
            Street = street;
            Country = countery ?? "not enterd";
        }

        public string FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}
