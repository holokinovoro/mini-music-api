using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.IRepository;
using Infrastructure.Repositories;
using System.Reflection;
using Serilog;
using Infrastructure;
using Application.Features.Queries.Song.GetSong;
using Application.Features.Commands.SongCommands.Update;

var builder = WebApplication.CreateBuilder(args);

//Logging
/*Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();
*/

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssemblies(typeof(GetAllSongsQueryHandler).Assembly, typeof(GetAllSongsQuery).Assembly,
        typeof(UpdateSongCommandHandler).Assembly, typeof(UpdateSongCommand).Assembly));



builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
