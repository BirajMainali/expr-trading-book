using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio_Management.Data;
using Portfolio_Management.Infrastructure.Providers;
using Portfolio_Management.Infrastructure.Providers.Interface;
using Portfolio_Management.Repository;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services;
using Portfolio_Management.Services.Interface;
using Portfolio_Management.Valuator;
using Portfolio_Management.Valuator.Interface;

namespace Portfolio_Management.Application
{
    public static class ApplicationDiConfig
    {
        public static void UseApplicationConfig(this IServiceCollection services)
        {
            UseRepos(services);
            UseMisc(services);
            UseServices(services);
        }

        private static void UseRepos(IServiceCollection service)
            => service.AddScoped<IStockRepository, StockRepository>()
                .AddScoped<IStockTransactionRepository, StockTransactionRepository>();

        private static void UseServices(IServiceCollection services)
            => services.AddScoped<IStockService, StockService>()
                .AddScoped<IStockTransactionService, StockTransactionService>();

        private static void UseMisc(IServiceCollection service)
            => service.AddScoped<DbContext, ApplicationDbContext>()
                .AddScoped<IStockValuator, StockValuator>()
                .AddScoped<ISqlConnectionProvider, SqlConnectionProvider>();
    }
}