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
    [Route("api/ServiceCategory")]
    [Authorize]
    public class ServiceCategorylistController : ControllerBase
    {
        private readonly HomeServicesContext _context;

        public ServiceCategorylistController(HomeServicesContext context)
        {
            _context = context;
        }

        // GET: api/cities/{cityId}/servicecategories
        [HttpGet("{cityId}/servicecategories")]
        public async Task<ActionResult<IEnumerable<ServiceCategoryDto>>> GetServiceCategoriesByCity(int cityId)
        {
            try
            {
                var serviceCategories = await _context.ServiceProvider
                    .Where(sp => sp.User.CityId == cityId)
                    .Select(sp => sp.ServiceCategory)
                    .Distinct()
                    .Select(sc => new ServiceCategoryDto
                    {
                        ServiceCategoryId = sc.ServiceCategoryId,
                        ServiceName = sc.ServiceName,
                        CategoryImage = $"/images/category/{sc.CategoryImage.Replace("\\", "/")}"
                    })
                    .ToListAsync();

                var response = new { serviceCategories = serviceCategories };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
