namespace HomeServices.Models
{
    public class ServiceProvider
    {
        public int ServiceProviderId { get; set; }
        public int UserId { get; set; }
        public string Bio { get; set; }
        public int ServiceCategoryId { get; set; }
        public decimal Rating { get; set; }
        public int Reviews { get; set; }
        public int ExperienceYear { get; set; }
        public decimal Price { get; set; }
        public string AvailabilityTimeSlot { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
    }
}
