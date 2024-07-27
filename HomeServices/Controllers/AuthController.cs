using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HomeServices.Data;
using HomeServices.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace HomeServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HomeServicesContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(HomeServicesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// User registration endpoint.
        /// </summary>
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserRegistrationDto userDto)
        {
            try
            {
                if (await _context.User.AnyAsync(u => u.Email == userDto.Email))
                {
                    return BadRequest("User already exists.");
                }

                var user = new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    Email = userDto.Email,
                    CityId = userDto.CityId,
                    CountryId = userDto.CountryId,
                    Address = userDto.Address,
                    ZipCode = userDto.ZipCode,
                    UserTypeId = userDto.UserTypeId,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// User login endpoint.
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userDto)
        {
            try
            {
                var user = _context.User.Include(u => u.UserType).SingleOrDefault(u => u.Email == userDto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password) || user.UserType.UserRole != userDto.UserType)
                {
                    return Unauthorized("Invalid email, password, or user type.");
                }

                var token = GenerateJwtToken(user);

                return Ok(new { token, userId = user.UserId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// User logout endpoint.
        /// </summary>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Invalidate the token if necessary (depends on token management strategy)
            return Ok(new { message = "Logged out successfully." });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userType", user.UserType.UserRole)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
