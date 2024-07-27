namespace HomeServices.Dto
{
    public class NearbyServiceProviderDto
    {
        public int ServiceProviderId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string ServiceCategory { get; set; }
        public decimal Price { get; set; }
        public string AvailabilityTimeSlot { get; set; }
        public string ServiceProviderProfilePicture { get; set; }
    }
}
