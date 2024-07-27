using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Emit;

namespace HomeServices.Models
{
    public class User
    {
        public int UserId { get; set; }
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
        public string ProfilePicture { get; set; } = "/images/Profile_Image/NoImage.jpg";
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation properties
        public City City { get; set; }
        public Country Country { get; set; }
        public UserType UserType { get; set; }
    }
}

