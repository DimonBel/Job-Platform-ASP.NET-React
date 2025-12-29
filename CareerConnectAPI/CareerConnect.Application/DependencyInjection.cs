using CareerConnect.Application.Interfaces;
using CareerConnect.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CareerConnect.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
