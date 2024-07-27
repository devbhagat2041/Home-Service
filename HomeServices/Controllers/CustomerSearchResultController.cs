using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Dto;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerSearchResultController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public CustomerSearchResultController(HomeServicesContext context)
        {
            _context = context;
        }

        // GET: api/customersearchresult/search-customer
        [HttpGet("search-customer")]
        public async Task<ActionResult<IEnumerable<CustomerSearchResultDto>>> SearchCustomerByName(int cityId, string customerName)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest("Invalid token. User email not found.");
                }

                var serviceProvider = await _context.ServiceProvider
                    .Include(sp => sp.User)
                    .FirstOrDefaultAsync(sp => sp.User.Email == userEmail);

                if (serviceProvider == null)
                {
                    return NotFound("Service provider profile not found.");
                }
                var customers = await _context.Booking
                    .Where(b => b.ServiceProviderId == serviceProvider.ServiceProviderId &&
                                b.Customer.CityId == cityId &&
                                (b.Customer.FirstName + " " + b.Customer.LastName).Contains(customerName))
                    .Select(b => new CustomerSearchResultDto
                    {
                        Name = $"{b.Customer.FirstName} {b.Customer.LastName}",
                        PhoneNumber = b.Customer.PhoneNumber,
                        Email = b.Customer.Email,
                        Address = b.Customer.Address,
                        ZipCode = b.Customer.ZipCode,
                        BookingDateTime = b.DateTime,
                        Description = b.Description,
                        Price = b.ServiceProvider.Price,
                        Status = b.Status.StatusName,
                        CustomerProfilePicture = b.Customer.ProfilePicture
                    })
                    .ToListAsync();

                if (customers.Count == 0)
                {
                    return NotFound("No customers found matching the search criteria in the selected city.");
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
