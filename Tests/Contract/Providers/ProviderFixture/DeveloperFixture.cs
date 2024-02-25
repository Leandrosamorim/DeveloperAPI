using IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tests.Contract.Providers.Middleware;

namespace Tests.Contract.Providers.ProviderFixture
{
    public class DeveloperApiFixture : IDisposable
    {
        /// <summary>
        /// Signing key used to create and validate JWT tokens used for testing
        /// </summary>
        /// <returns></returns>
        public static readonly SymmetricSecurityKey IssuerSigningKey = new(Encoding.UTF8.GetBytes("thisIsASecretKey"));
        /// <summary>
        /// URI used for hosting the tests
        /// </summary>
        /// <value></value>
        public Uri ServerUri { get; }

        private readonly IHost server;
        private const string TEST_ENVIRONMENT = "CONTRACT";

        /// <summary>
        /// Setup eSign DocuSign API fixture to host application for testing
        /// </summary>
        public DeveloperApiFixture()
        {
            ServerUri = new Uri("http://localhost:9223");
            server = Host.CreateDefaultBuilder().
                         UseEnvironment(TEST_ENVIRONMENT).
                         ConfigureWebHostDefaults(hostBuilder =>
                         {
                             hostBuilder.UseUrls(ServerUri.ToString());
                             hostBuilder.ConfigureServices((context, services) =>
                             {
                                 services.AddControllers();
                                 services.AddEndpointsApiExplorer();
                                 services.AddInfrastructure(context.Configuration, context.HostingEnvironment);
                                 //services.AddCors(options =>
                                 //{
                                 //    options.AddPolicy("MatchAPI", b => b.WithOrigins("http://localhost:5262/").AllowAnyHeader().AllowAnyMethod());
                                 //});

                                 services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                                 {
                                     options.TokenValidationParameters = new TokenValidationParameters
                                     {
                                         ValidateIssuer = true,
                                         ValidateAudience = true,
                                         ValidateLifetime = true,
                                         ValidateIssuerSigningKey = true,
                                         ValidIssuer = "baseWebApiIssuer",
                                         ValidAudience ="baseWebApiAudience",
                                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JsonWebApiTokenWithSwaggerAuthorizationAuthenticationAspNetCore"))
                                     };
                                 });
                                 services.AddAuthorization();
                                 
                             });
                             hostBuilder.Configure(app =>
                             {
                                 app.UseMiddleware<AuthorizationMiddleware>().
                                     UseMiddleware<ProviderStateMiddleware>();
                                 app.UseRouting();
                                 app.UseAuthentication();
                                 app.UseAuthorization();
                                 app.UseEndpoints(e => e.MapControllers());
                                 //app.UseCors();
                             });
                         }).
                         Build();

            server.Start();
        }

        public void Dispose()
        {
            server.Dispose();
        }
    }

}
