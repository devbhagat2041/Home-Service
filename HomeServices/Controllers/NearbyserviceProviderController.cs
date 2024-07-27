using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Models;
using HomeServices.Dto;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/serviceproviders")]
    [Authorize]
    public class NearbyserviceProviderController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public NearbyserviceProviderController(HomeServicesContext context)
        {
            _context = context;
        }


        // GET: api/cities/{cityId}/serviceproviders/all
        [HttpGet("{cityId}/serviceproviders/all")]
        public async Task<ActionResult<IEnumerable<NearbyServiceProviderDto>>> GetAllServiceProvidersByCity(int cityId)
        {
            try
            {
                var serviceProviders = await _context.ServiceProvider
                    .Where(sp => sp.User.CityId == cityId)
                    .Select(sp => new NearbyServiceProviderDto
                    {
                        ServiceProviderId = sp.ServiceProviderId,
                        Name = $"{sp.User.FirstName} {sp.User.LastName}",
                        PhoneNumber = sp.User.PhoneNumber,
                        Email = sp.User.Email,
                        Bio = sp.Bio,
                        ServiceCategory = sp.ServiceCategory.ServiceName,
                        Price = sp.Price,
                        AvailabilityTimeSlot = sp.AvailabilityTimeSlot,
                        ServiceProviderProfilePicture = sp.User.ProfilePicture

                    })
                    .ToListAsync();

                var response = new { serviceProviders = serviceProviders };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET: api/serviceproviders/searchresult/{cityId}?search={search}
        [HttpGet("searchresult")]
        public async Task<ActionResult<IEnumerable<NearbyServiceProviderDto>>> SearchServiceProviders(int cityId, [FromQuery] string search)
        {
            try
            {
                var serviceProviders = await _context.ServiceProvider
                    .Where(sp => sp.User.CityId == cityId &&
                                 (sp.ServiceCategory.ServiceName.Contains(search) ||
                                  (sp.User.FirstName + " " + sp.User.LastName).Contains(search)))
                    .Select(sp => new NearbyServiceProviderDto
                    {
                        ServiceProviderId = sp.ServiceProviderId,
                        Name = $"{sp.User.FirstName} {sp.User.LastName}",
                        PhoneNumber = sp.User.PhoneNumber,
                        Email = sp.User.Email,
                        Bio = sp.Bio,
                        ServiceCategory = sp.ServiceCategory.ServiceName,
                        Price = sp.Price,
                        AvailabilityTimeSlot = sp.AvailabilityTimeSlot,
                        ServiceProviderProfilePicture = sp.User.ProfilePicture
                    })
                    .ToListAsync();

                var response = new { serviceProviders = serviceProviders };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
