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
    [Route("api/countries")]
    public class CountrylistController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public CountrylistController(HomeServicesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countries = await _context.Country
                .Select(c => new CountryDto
                {
                    CountryId = c.CountryId,
                    CountryName = c.CountryName
                }).ToListAsync();

            var response = new { countries = countries };

            return Ok(response);
        }
    }
}
