using Application.Services;
using Cqrs.Hosts;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MusicAPI.Extensions;

public static class ApiExtentions
{
    public static void AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();



        services.
            AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["pookie-cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        services.AddAuthorization();
    }

    public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
    this TBuilder builder, params Permission[] permissions)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder
            .RequireAuthorization(pb =>
                pb.AddRequirements(new PermissionRequirement(permissions)));
    }

    

}
