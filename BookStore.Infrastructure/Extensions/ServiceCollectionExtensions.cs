using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Auth;
using BookStore.Infrastructure.Settings;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace BookStore.Infrastructure.Extensions;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<BookStoreDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Settings
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // Repositories
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


        // Auth helpers
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        services.AddSingleton<IPasswordHasher, PasswordHasherAdapter>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();


        return services;
    }
}