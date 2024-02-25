using Domain.AuthNS;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tests.Contract.Providers.Middleware
{
    public class AuthorizationMiddleware
    {
        private const string Authorization = "Authorization";
        private readonly RequestDelegate next;

        public AuthorizationMiddleware(RequestDelegate next) 
        { 
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Headers.ContainsKey(Authorization))
            {
                string token = GenerateToken();
                context.Request.Headers[Authorization] = $"Bearer {token}";
            }
            await this.next(context);
        }

        private string GenerateToken()
        {
            var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, "baseWebApiSubject"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("UId", Guid.NewGuid().ToString(), null),
                        new Claim("Username", "login" ),
                        new Claim("Password", "password")
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JsonWebApiTokenWithSwaggerAuthorizationAuthenticationAspNetCore"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                "baseWebApiIssuer",
                "baseWebApiAudience",
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signIn
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
