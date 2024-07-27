using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Models;
using HomeServices.Dto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public BookingsController(HomeServicesContext context)
        {
            _context = context;
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var booking = new Booking
                {
                    CustomerId = bookingDto.CustomerId,
                    ServiceProviderId = bookingDto.ServiceProviderId,
                    DateTime = bookingDto.DateTime,
                    Description = bookingDto.Description,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    StatusId = 105
                };

                _context.Booking.Add(booking);
                await _context.SaveChangesAsync();

                var responseObject = new
                {
                    bookingId = booking.BookingId,
                    customerId = booking.CustomerId,
                    serviceProviderId = booking.ServiceProviderId,
                    dateTime = booking.DateTime,
                    description = booking.Description,
                    StatusId = booking.StatusId
                };

                return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, responseObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/bookings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetailsDto>> GetBookingById(int id)
        {
            var booking = await _context.Booking
         .Where(b => b.BookingId == id)
         .Include(b => b.ServiceProvider)
         .ThenInclude(sp => sp.User)
         .Select(b => new BookingDetailsDto
         {
             BookingId = b.BookingId,
             CustomerId = b.CustomerId,
             Customer = $"{b.Customer.FirstName} {b.Customer.LastName}",
             ServiceProviderId = b.ServiceProviderId,
             ServiceProvider = $"{b.ServiceProvider.User.FirstName} {b.ServiceProvider.User.LastName}",
             City = b.Customer.City.CityName,
             DateTime = b.DateTime,
             Description = b.Description,
             ServiceCategory = b.ServiceProvider.ServiceCategory.ServiceName,
             Price = b.ServiceProvider.Price,
             StatusId = b.StatusId,
             CustomerProfilePicture = b.Customer.ProfilePicture,
             ServiceProviderProfilePicture = b.ServiceProvider.User.ProfilePicture,
         })
                .FirstOrDefaultAsync();

            var response = new { booking = booking };

            return Ok(response);
        }


        // GET: api/bookings/listall
        [HttpGet("listall")]
        public async Task<ActionResult<IEnumerable<BookingDetailsDto>>> GetAllBookings()
        {
            var bookings = await _context.Booking
                .Include(b => b.ServiceProvider)
                .ThenInclude(sp => sp.User) // Include ServiceProvider's User
                .Select(b => new BookingDetailsDto
                {
                    BookingId = b.BookingId,
                    CustomerId = b.CustomerId,
                    Customer = $"{b.Customer.FirstName} {b.Customer.LastName}",
                    ServiceProviderId = b.ServiceProviderId,
                    ServiceProvider = $"{b.ServiceProvider.User.FirstName} {b.ServiceProvider.User.LastName}", // Construct full name
                    City = b.Customer.City.CityName,
                    DateTime = b.DateTime,
                    Description = b.Description,
                    ServiceCategory = b.ServiceProvider.ServiceCategory.ServiceName,
                    Price = b.ServiceProvider.Price,
                    StatusId = b.StatusId,
                    CustomerProfilePicture = b.Customer.ProfilePicture,
                    ServiceProviderProfilePicture = b.ServiceProvider.User.ProfilePicture,
                })
                .ToListAsync();

            var response = new { bookings = bookings };

            return Ok(response);
        }


        // GET: api/bookings/manage
        [HttpGet("service Provider/Booking-Request-List")]
        public async Task<ActionResult<IEnumerable<BookingDetailsDto>>> GetBookingsByServiceProviderAndCity(int serviceProviderId, int cityId)
        {
            var bookings = await _context.Booking
        .Where(b => b.ServiceProviderId == serviceProviderId && b.StatusId == 105 && b.Customer.CityId == cityId)
        .Include(b => b.Customer)
            .ThenInclude(c => c.City)
        .Include(b => b.ServiceProvider)
            .ThenInclude(sp => sp.ServiceCategory)
        .Include(b => b.ServiceProvider)
            .ThenInclude(sp => sp.User)
        .Select(b => new BookingDetailsDto
        {
            BookingId = b.BookingId,
            CustomerId = b.CustomerId,
            Customer = $"{b.Customer.FirstName} {b.Customer.LastName}",
            ServiceProviderId = b.ServiceProviderId,
            ServiceProvider = $"{b.ServiceProvider.User.FirstName} {b.ServiceProvider.User.LastName}",
            City = b.Customer.City.CityName,
            DateTime = b.DateTime,
            Description = b.Description,
            ServiceCategory = b.ServiceProvider.ServiceCategory.ServiceName,
            Price = b.ServiceProvider.Price,
            StatusId = b.StatusId,
            Address = b.ServiceProvider.User.Address,  
            ZipCode = b.ServiceProvider.User.ZipCode,  
            CustomerProfilePicture = b.Customer.ProfilePicture,
            ServiceProviderProfilePicture = b.ServiceProvider.User.ProfilePicture,
        })
                .ToListAsync();

            var response = new { bookings = bookings };

            return Ok(response);
        }


        // PUT: api/bookings/serviceProvider/{BookingId}/accept
        [HttpPut("serviceProvider/{BookingId}/accept")]
        public async Task<IActionResult> AcceptBooking(int BookingId)
        {
            var booking = await _context.Booking.FindAsync(BookingId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            if (booking.StatusId == 105) //Scheduled
            {
                booking.StatusId = 101; // Pending status
                booking.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Status changed to Pending" });
            }
            else
            {
                return Ok(new { message = "Booking status is not Scheduled." });
            }
        }

        // PUT: api/bookings/serviceProvider/{BookingId}/completeed
        [HttpPut("serviceProvider/{BookingId}/complete")]
        public async Task<IActionResult> CompleteBooking(int BookingId)
        {
            var booking = await _context.Booking.FindAsync(BookingId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            if (booking.StatusId == 101) // Assuming 101 is the Pending status
            {
                booking.StatusId = 102; // Completed status
                booking.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Status changed to Completed" });
            }
            else
            {
                return BadRequest(new { message = "Booking status is not Pending." });
            }
        }



        // PUT: api/bookings/serviceProvider/{BookingId}/reject
        [HttpPut("serviceProvider/{BookingId}/reject")]
        public async Task<IActionResult> RejectBooking(int BookingId)
        {
            var booking = await _context.Booking.FindAsync(BookingId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            if (booking.StatusId == 105) //Scheduled
            {
                booking.StatusId = 103; // Rejected status
                booking.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Status changed to Rejected." });
            }
            else
            {
                return Ok(new { message = "Booking status is not Scheduled." });
            }
        }


        // PUT: api/bookings/customer/{BookingId}/cancel
        [HttpPut("customer/{BookingId}/cancel")]
        public async Task<IActionResult> CancelBooking(int BookingId)
        {
            var booking = await _context.Booking.FindAsync(BookingId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Assuming only Pending or Accepted bookings can be cancelled
            if (booking.StatusId == 105 || booking.StatusId == 101)
            {
                booking.StatusId = 104; // Cancelled status
                booking.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Booking has been cancelled." });
            }
            else
            {
                return Ok(new { message = "Only Pending or Accepted bookings can be cancelled." });
            }
        }

    }
}
