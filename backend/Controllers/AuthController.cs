using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_dotnet.Models;

namespace api_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        private User AuthenticateUser(Login login)
        {

            User user = null;
            // ATTENTION : On sait que c'est pas cool de mettre en dur mais c'était plus simple (et autorisé pour cette fois-ci) :)
            if (login.Username == "borloo" && login.Password == "borloo")
            {
                user = new User { Username = "borloo", EmailAddress = "borloo@gmail.com", Role = "admin" };
            }
            return user;
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
            new Claim("role", user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
