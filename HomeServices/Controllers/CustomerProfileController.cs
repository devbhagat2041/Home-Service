using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using HomeServices.Dto;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/profile")]
    [Authorize]
    public class CustomerProfileController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public CustomerProfileController(HomeServicesContext context)
        {
            _context = context;
        }

        [HttpGet("customer")]
        public async Task<ActionResult<CustomerProfileDto>> GetCustomerProfile()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("Invalid token. User email not found.");
            }

            var userProfile = await _context.User
                .Include(u => u.City)
                .Include(u => u.Country)
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            var customerProfileDto = new CustomerProfileDto
            {
                UserId = userProfile.UserId,
                Name = $"{userProfile.FirstName} {userProfile.LastName}",
                PhoneNumber = userProfile.PhoneNumber,
                Email = userProfile.Email,
                CityId = userProfile.CityId,
                City = userProfile.City?.CityName,
                CountryId = userProfile.CountryId,
                Country = userProfile.Country?.CountryName, 
                Address = userProfile.Address,
                ZipCode = userProfile.ZipCode,
                ProfilePicture = userProfile.ProfilePicture
            };

            var response = new { customerProfile = customerProfileDto };

            return Ok(response);
        }


        [HttpPut("customer-edit-profile")]
        public async Task<ActionResult> UpdateCustomerProfile([FromForm] CustomerUpdateDto updateDto)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest("Invalid token. User email not found.");
                }

                var user = await _context.User
                    .FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return NotFound("User profile not found.");
                }

                if (!string.IsNullOrEmpty(updateDto.Name))
                {
                    var nameParts = updateDto.Name.Split(' ', 2);
                    if (nameParts.Length < 2)
                    {
                        return BadRequest("Name must include both first and last name.");
                    }
                    user.FirstName = nameParts[0];
                    user.LastName = nameParts[1];
                }

                if (!string.IsNullOrEmpty(updateDto.Address))
                {
                    user.Address = updateDto.Address;
                }

                if (!string.IsNullOrEmpty(updateDto.ZipCode))
                {
                    user.ZipCode = updateDto.ZipCode;
                }

                if (updateDto.ProfilePicture != null && updateDto.ProfilePicture.Length > 0)
                {
                    var originalFileName = Path.GetFileName(updateDto.ProfilePicture.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(originalFileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Profile_Image/", originalFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateDto.ProfilePicture.CopyToAsync(stream);
                    }

                    user.ProfilePicture = $"/images/Profile_Image/{originalFileName}";
                }

                user.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Customer profile updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
