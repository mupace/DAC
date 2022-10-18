using DAC.API.Models.AuthModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAC.API.Models.Settings;
using Microsoft.Extensions.Options;

namespace DAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public readonly IOptions<AuthConfiguration> _authConfiguration;

        public AuthController(IOptions<AuthConfiguration> authConfiguration)
        {
            _authConfiguration = authConfiguration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return BadRequest("Invalid user request!!!");
            }
            
            if (apiKey == _authConfiguration.Value.ApiKey)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.Value.JWT.Secret));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _authConfiguration.Value.JWT.ValidIssuer,
                    audience: _authConfiguration.Value.JWT.ValidAudience,
                    claims: new List<Claim>() { new Claim(ClaimTypes.Name, "memo") },
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
