using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServices.Data;
using HomeServices.Models;
using HomeServices.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/ratings")]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public RatingsController(HomeServicesContext context)
        {
            _context = context;
        }

        // POST: api/ratings
        [HttpPost]
        public async Task<IActionResult> SubmitRating([FromBody] RatingDto ratingDto)
        {
            if (ratingDto == null || ratingDto.Ratings < 1 || ratingDto.Ratings > 5)
            {
                return BadRequest("Invalid data.");
            }

            // Check if the booking exists
            var booking = await _context.Booking
                .FirstOrDefaultAsync(b => b.BookingId == ratingDto.BookingId);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Check if the service provider exists
            var serviceProvider = await _context.ServiceProvider
                .FirstOrDefaultAsync(sp => sp.ServiceProviderId == ratingDto.ServiceProviderId);

            if (serviceProvider == null)
            {
                return NotFound("Service provider not found.");
            }

            // Create and save the rating
            var rating = new Rating
            {
                BookingId = ratingDto.BookingId,
                ServiceProviderId = ratingDto.ServiceProviderId,
                Ratings = ratingDto.Ratings,
                Reviews = ratingDto.Reviews,
                ServiceCategoryId = ratingDto.ServiceCategoryId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Rating.Add(rating);
            await _context.SaveChangesAsync();

            // Update the service provider's rating and review count
            var ratings = await _context.Rating
                .Where(r => r.ServiceProviderId == ratingDto.ServiceProviderId)
                .ToListAsync();

            if (ratings.Any())
            {
                var averageRating = ratings.Average(r => r.Ratings);
                serviceProvider.Rating = averageRating;
                serviceProvider.Reviews = ratings.Count;
                serviceProvider.ModifiedDate = DateTime.UtcNow;

                _context.ServiceProvider.Update(serviceProvider);
                await _context.SaveChangesAsync();
            }

            return Ok(rating);
        }
    }
}
