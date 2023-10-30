using System.Text.Json.Serialization;
using backend.Application.Repositories;
using backend.Application.Services;
using backend.Infrastructure;
using backend.Infrastructure.Repositories;
using backend.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("yu-gi-oh-postgres-database")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// seeders
builder.Services.AddScoped<ISeedCommand, LocalizationSeed>();
builder.Services.AddScoped<ISeedCommand, CardSeed>();

// services
builder.Services.AddScoped<ProvinceService>();
builder.Services.AddScoped<MunicipalityService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<ArchetypeService>();

// repositories
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
builder.Services.AddScoped<ICardRepository,CardRepository>();
builder.Services.AddScoped<IArchetypeRepository, ArchetypeRepository>();

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
