using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Models;
using HomeServices.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/serviceprovider")]
    [Authorize]
    public class CompletedTasksController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public CompletedTasksController(HomeServicesContext context)
        {
            _context = context;
        }

        // GET: api/serviceprovider/completedtasks/{serviceProviderId}/{cityId}
        [HttpGet("completedtasks/{serviceProviderId}/{cityId}")]
        public async Task<ActionResult<IEnumerable<CompletedTaskDto>>> GetCompletedTasks(int serviceProviderId, int cityId)
        {
            try
            {
                var completedTasks = await _context.Booking
                    .Where(b => b.ServiceProviderId == serviceProviderId
                                 && b.Customer.CityId == cityId
                                && b.Status.StatusName == "Completed")
                    .Select(b => new CompletedTaskDto
                    {
                        BookingId = b.BookingId,
                        CustomerId = b.CustomerId,
                        Customer = $"{b.Customer.FirstName} {b.Customer.LastName}",
                        City = b.Customer.City.CityName,
                        DateTime = b.DateTime,
                        Description = b.Description,
                        ServiceCategory = b.ServiceProvider.ServiceCategory.ServiceName,
                        Price = b.ServiceProvider.Price,
                        Status = b.Status.StatusName,
                        CustomerProfilePicture = b.Customer.ProfilePicture
                    })
                    .ToListAsync();

                var response = new { completedTasks = completedTasks };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
