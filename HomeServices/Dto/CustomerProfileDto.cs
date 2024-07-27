namespace HomeServices.Dto
{
    public class CustomerProfileDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string ProfilePicture { get; set; }
    }
    public class CustomerUpdateDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }

}

