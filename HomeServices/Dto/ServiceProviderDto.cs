using System.ComponentModel.DataAnnotations;

namespace HomeServices.Dto
{
    public class ServiceProviderDto
    {
        public int ServiceProviderId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Bio { get; set; }
        public string ServiceCategory { get; set; }
        public decimal Price { get; set; }
        public int ExperienceYear { get; set; }
        public string AvailabilityTimeSlot { get; set; }
        public string ProfilePicture { get; set; }
    }
    public class ServiceProvideraddDto
    {
        public string Bio { get; set; }
        public int ServiceCategoryId { get; set; }
        public decimal Price { get; set; }
        public string AvailabilityTimeSlot { get; set; }
        public int ExperienceYear { get; set; }
    }

    public class ServiceProviderUpdateDto
    {
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public int? ServiceCategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? AvailabilityTimeSlot { get; set; }
        public int? ExperienceYear { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }










}
