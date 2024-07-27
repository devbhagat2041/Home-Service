namespace HomeServices.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public decimal Ratings { get; set; }
        public int BookingId { get; set; }
        public string Reviews { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public Booking Booking { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
    }
}
