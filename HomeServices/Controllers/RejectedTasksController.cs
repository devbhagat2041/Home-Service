using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Models;
using HomeServices.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/serviceprovider")]
    [Authorize]
    public class RejectedTasksController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public RejectedTasksController(HomeServicesContext context)
        {
            _context = context;
        }

        // GET: api/serviceprovider/rejectedtasks/{serviceProviderId}/{cityId}
        [HttpGet("rejectedtasks/{serviceProviderId}/{cityId}")]
        public async Task<ActionResult<IEnumerable<RejectedTaskDto>>> GetRejectedTasks(int serviceProviderId, int cityId)
        {
            try
            {
                var rejectedTasks = await _context.Booking
                    .Where(b => b.ServiceProviderId == serviceProviderId
                                 && b.Customer.CityId == cityId
                                && b.Status.StatusName == "Rejected")
                    .Select(b => new RejectedTaskDto
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

                var response = new { rejectedTasks = rejectedTasks };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
