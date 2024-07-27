namespace HomeServices.Dto
{
    public class RatingDto
    {
        public int BookingId { get; set; }
        public int ServiceProviderId { get; set; }
        public decimal Ratings { get; set; }
        public string Reviews { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}
