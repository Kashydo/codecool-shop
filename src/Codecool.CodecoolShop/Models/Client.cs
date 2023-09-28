namespace Codecool.CodecoolShop.Models
{
    public class Client : BaseModel
    {
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Streat { get; set; }

        public string HouseNumber { get; set; }

        public string? FlatNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


    }
}
