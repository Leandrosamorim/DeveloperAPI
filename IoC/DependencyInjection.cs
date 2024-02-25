using Data.Context;
using Data.Repositories;
using Domain.DeveloperNS.Interface;
using Domain.DeveloperNS.Service;
using Domain.HttpService;
using Domain.HttpService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddTransient<IMatchHttpService, MatchHttpService>();

            if(environment.IsDevelopment() || environment.IsProduction())
            {
                services.AddDbContext<DeveloperDBContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                services.AddDbContext<DeveloperDBContext>(options => options.UseInMemoryDatabase("DeveloperContext"));
            }
            return services;
        }
    }
}
