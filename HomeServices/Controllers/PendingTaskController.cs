using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeServices.Dto;
using HomeServices.Data;

namespace HomeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingTaskController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public PendingTaskController(HomeServicesContext context)
        {
            _context = context;
        }

        // GET: api/pendingtask/pendingtasks/{serviceProviderId}/{cityId}
        [HttpGet("pendingtasks/{serviceProviderId}/{cityId}")]
        public async Task<ActionResult<IEnumerable<PendingTaskDto>>> GetPendingTasks(int serviceProviderId, int cityId)
        {
            try
            {
                var pendingTasks = await _context.Booking
                    .Where(b => b.ServiceProviderId == serviceProviderId
                                 && b.Customer.CityId == cityId
                                && b.Status.StatusName == "Pending")
                    .Select(b => new PendingTaskDto
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

                var response = new { pendingTasks = pendingTasks };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
