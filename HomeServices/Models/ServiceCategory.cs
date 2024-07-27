namespace HomeServices.Models
{
    public class ServiceCategory
    {
        public int ServiceCategoryId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public int DurationTimeSlot { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CategoryImage { get; set; }
    }
}
