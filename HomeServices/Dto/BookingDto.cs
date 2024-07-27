namespace HomeServices.Dto
{
    public class BookingDto
    {
        public int CustomerId { get; set; }
        public int ServiceProviderId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
    }

    public class BookingDetailsDto
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public int ServiceProviderId { get; set; }
        public string ServiceProvider { get; set; }
        public string City { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string ServiceCategory { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string CustomerProfilePicture { get; set; }
        public string ServiceProviderProfilePicture { get; set; }
    }
}
