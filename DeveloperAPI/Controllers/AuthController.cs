using Domain.AuthNS;
using Domain.DeveloperNS;
using Domain.DeveloperNS.Command;
using Domain.DeveloperNS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeveloperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDeveloperService _developerService;

        public AuthController(IConfiguration configuration, IDeveloperService developerService)
        {
            _configuration = configuration;
            _developerService = developerService;
        }


        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(DeveloperLoginCmd cmd)
        {
            if (cmd == null)
                return BadRequest("Invalid User");
            else
            {
                var userData = await GetDeveloper(cmd);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UId", userData.UId.ToString(), null),
                        new Claim("Username", userData.Login ),
                        new Claim("Password", userData.Password)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                    return BadRequest("User not found");
            }
        }
        [HttpGet]
        public async Task<Developer> GetDeveloper(DeveloperLoginCmd cmd)
        {
            return await _developerService.Login(cmd);
        }
    }
}
