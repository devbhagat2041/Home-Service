namespace HomeServices.Models
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int UserTypeId { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }


}
