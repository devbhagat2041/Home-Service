namespace HomeServices.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceProviderId { get; set; }
        public DateTime DateTime { get; set; }
        public int StatusId { get; set; } = 105;
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public User Customer { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public Status Status { get; set; }
    }
}
