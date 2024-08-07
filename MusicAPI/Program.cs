using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Interfaces.IRepository;
using Infrastructure.Repositories;
using System.Reflection;
using Serilog;
using Application.Features.Queries.Song.GetSong;
using Application.Features.Commands.SongCommands.Update;
using Application.Services;
using Infrastructure.Services;
using Application.Interfaces.Auth;
using MusicAPI.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authorization;
using AuthorizationOptions = Infrastructure.AuthorizationOptions;
using System.Net;
using System;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration).CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure CORS to allow any method, origin, and header
builder.Services.AddCors(opts => opts.AddDefaultPolicy(opts => opts.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()));

builder.Services.AddSwaggerGen();
builder.Services.AddSerilog();

//Application and Infrastructure services registration

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using(var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<DataContext>();
    context.Database.Migrate();
}

app.Run();
