using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio_Management.Data;
using Portfolio_Management.Repository;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services;
using Portfolio_Management.Services.Interface;
using Portfolio_Management.Valuator;
using Portfolio_Management.Valuator.Interface;

namespace Portfolio_Management
{
    public static class DiConfig
    {
        public static IServiceCollection UseDiConfig(this IServiceCollection services, IConfiguration configuration)
        {
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
            UseRepos(services);
            UseServices(services);
            UseMisc(services);
            return services;
        }

        private static void UseRepos(IServiceCollection service)
            => service.AddScoped<IStockRepository, StockRepository>()
                .AddScoped<IStockTransactionRepository, StockTransactionRepository>();

        private static void UseServices(IServiceCollection services)
            => services.AddScoped<IStockService, StockService>()
                .AddScoped<IStockTransactionService, StockTransactionService>();

        private static void UseMisc(IServiceCollection service)
            => service.AddScoped<DbContext, ApplicationDbContext>()
                .AddScoped<IStockValuator, StockValuator>();
    }
}