using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DeveloperTest.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClearBankServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var dataStoreType = configuration["DataStoreType"];

        if (dataStoreType == "Backup")
        {
            services.AddScoped<IAccountDataStore, BackupAccountDataStore>();
        }
        else
        {
            services.AddScoped<IAccountDataStore, AccountDataStore>();
        }
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentStrategyProvider, PaymentStrategyProvider>();
        
        return services;
    }
}