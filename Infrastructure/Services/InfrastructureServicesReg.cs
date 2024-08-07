﻿using Application.Interfaces.Auth;
using Application.Interfaces.IRepository;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Mappings;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Services;

public static class InfrastructureServicesReg
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();

        services.AddScoped<SongRepository>();
        services.AddScoped<ISongRepository>(provider =>
        {
            var songRepository = provider.GetService<SongRepository>();

            return new CashedSongRepository(
                songRepository!,
                provider.GetService<IMemoryCache>()!);
        });

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddAutoMapper(typeof(DbMappings));

        return services;
    }
}
