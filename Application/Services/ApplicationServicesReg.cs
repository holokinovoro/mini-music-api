using Application.Common.Behaviors;
using Application.Features.Commands.SongCommands.Update;
using Application.Features.Queries.Song.GetSong;
using Application.Interfaces.Auth;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class ApplicationServicesReg
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                   Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);

            services.AddMemoryCache();

            services.AddScoped<UserService>();
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));


            return services;
        }
    }
}
