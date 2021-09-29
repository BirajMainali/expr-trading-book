using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio_Management.Data;

namespace Portfolio_Management.Application
{
    public static class DiConfig
    {
        public static IServiceCollection UseDiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.UseApplicationConfig();
            services.AddCors(x =>
            {
                x.AddDefaultPolicy(b =>
                {
                    b.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}