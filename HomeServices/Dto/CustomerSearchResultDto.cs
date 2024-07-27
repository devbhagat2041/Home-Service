namespace HomeServices.Dto
{
    public class CustomerSearchResultDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public DateTime BookingDateTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string CustomerProfilePicture { get; set; }
    }
}
