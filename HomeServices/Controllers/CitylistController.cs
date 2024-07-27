using HomeServices.Data;
using HomeServices.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitylistController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public CitylistController(HomeServicesContext context)
        {
            _context = context;
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(int countryId)
        {
            var cities = await _context.City
                .Where(c => c.CountryId == countryId)
                .Select(c => new CityDto
                {
                    CityId = c.CityId,
                    CityName = c.CityName,
                    CountryId = c.CountryId
                }).ToListAsync();

            var response = new { cities = cities };

            return Ok(response);
        }
    }
}
