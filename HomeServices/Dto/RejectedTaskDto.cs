namespace HomeServices.Dto
{
    public class RejectedTaskDto
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string City { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string ServiceCategory { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string CustomerProfilePicture { get; set; }
    }
}
