using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using HomeServices.Dto;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/profile")]
    [Authorize] // Ensure only authenticated users can access this endpoint
    public class ServiceProviderProfileController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public ServiceProviderProfileController(HomeServicesContext context)
        {
            _context = context;
        }

        [HttpGet("serviceprovider")]
        public async Task<ActionResult<ServiceProviderDto>> GetServiceProviderProfile()
        {
            try
            {
                // Retrieve user's email from the token
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest("Invalid token. User email not found.");
                }

                var serviceProvider = await _context.ServiceProvider
                    .Include(sp => sp.User)
                        .ThenInclude(u => u.City)
                    .Include(sp => sp.User)
                        .ThenInclude(u => u.Country)
                    .Include(sp => sp.ServiceCategory)
                    .FirstOrDefaultAsync(sp => sp.User.Email == userEmail);

                var serviceProviderDto = new ServiceProviderDto
                {
                    ServiceProviderId = serviceProvider.ServiceProviderId,
                    Name = $"{serviceProvider.User.FirstName} {serviceProvider.User.LastName}",
                    PhoneNumber = serviceProvider.User.PhoneNumber,
                    Email = serviceProvider.User.Email,
                    CityId = serviceProvider.User.CityId,
                    City = serviceProvider.User.City?.CityName,
                    CountryId = serviceProvider.User.CountryId,
                    Country = serviceProvider.User.Country?.CountryName,
                    Address = serviceProvider.User.Address,
                    ZipCode = serviceProvider.User.ZipCode,
                    Bio = serviceProvider.Bio,
                    ServiceCategory = serviceProvider.ServiceCategory?.ServiceName,
                    Price = serviceProvider.Price,
                    ExperienceYear= serviceProvider.ExperienceYear,
                    AvailabilityTimeSlot = serviceProvider.AvailabilityTimeSlot,
                    ProfilePicture = serviceProvider.User.ProfilePicture
                };

                var response = new { ServiceProviderProfile = serviceProviderDto };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("serviceprovider-Add-profile")]
        public async Task<ActionResult> AddServiceProviderProfile([FromBody] ServiceProvideraddDto updateDto)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return Ok("Invalid token. User email not found.");
                }

                var user = await _context.User
                    .FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return Ok("User not found.");
                }

                var serviceProvider = await _context.ServiceProvider
                    .FirstOrDefaultAsync(sp => sp.UserId == user.UserId);

                if (serviceProvider == null)
                {
                    serviceProvider = new HomeServices.Models.ServiceProvider
                    {
                        UserId = user.UserId,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.ServiceProvider.Add(serviceProvider);
                }
                serviceProvider.Bio = updateDto.Bio;
                serviceProvider.ServiceCategoryId = updateDto.ServiceCategoryId;
                serviceProvider.Price = updateDto.Price;
                serviceProvider.AvailabilityTimeSlot = updateDto.AvailabilityTimeSlot;
                serviceProvider.ExperienceYear = updateDto.ExperienceYear;
                serviceProvider.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Service provider profile successfully updated." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("serviceprovider-update-profile")]
        public async Task<ActionResult> UpdateServiceProviderProfile([FromForm] ServiceProviderUpdateDto updateDto)
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
                    return NotFound("User not found.");
                }
                var serviceProvider = await _context.ServiceProvider
                    .FirstOrDefaultAsync(sp => sp.UserId == user.UserId);

                if (serviceProvider == null)
                {
                    serviceProvider = new HomeServices.Models.ServiceProvider
                    {
                        UserId = user.UserId,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.ServiceProvider.Add(serviceProvider);
                }

                if (!string.IsNullOrEmpty(updateDto.Name))
                {
                    var nameParts = updateDto.Name.Split(' ', 2);
                    if (nameParts.Length >= 2)
                    {
                        user.FirstName = nameParts[0];
                        user.LastName = nameParts[1];
                    }
                }

                if (!string.IsNullOrEmpty(updateDto.Bio))
                {
                    serviceProvider.Bio = updateDto.Bio;
                }

                if (updateDto.ServiceCategoryId.HasValue)
                {
                    serviceProvider.ServiceCategoryId = updateDto.ServiceCategoryId.Value;
                }

                if (updateDto.Price.HasValue)
                {
                    serviceProvider.Price = updateDto.Price.Value;
                }

                if (!string.IsNullOrEmpty(updateDto.AvailabilityTimeSlot))
                {
                    serviceProvider.AvailabilityTimeSlot = updateDto.AvailabilityTimeSlot;
                }

                if (updateDto.ExperienceYear.HasValue)
                {
                    serviceProvider.ExperienceYear = updateDto.ExperienceYear.Value;
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

                serviceProvider.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Service provider profile successfully updated." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}

