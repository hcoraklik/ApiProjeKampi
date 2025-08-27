using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AuthController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous] // Login için yetki istemiyoruz
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == loginDto.Username && x.Password == loginDto.Password);

            if (user == null)
                return BadRequest("Kullanıcı adı veya şifre yanlış");

            // 1) Claim'leri hazırla (isteğe göre çoğaltabilirsin)
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, "Admin")
    };

            // 2) Anahtar ve imza bilgileri
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3) Süre, issuer, audience
            var expires = DateTime.UtcNow.AddMinutes(
                double.TryParse(_configuration["Jwt:ExpireMinutes"], out var m) ? m : 60);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = creds
            };

            // 4) Token'ı üret ve yaz
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            var tokenString = handler.WriteToken(token);

            // 5) JSON olarak token'ı ve bitiş saatini döndür
            return Ok(new
            {
                AccessToken = tokenString,
               
            });
        }
        [HttpGet]
        
        public IActionResult HelloWorld()
            {
            return Ok ("HelloWorld");
            }

        


        









    }
}
