using System.Text.Json.Serialization;
using backend.Application.Providers;
using backend.Application.Repositories;
using backend.Application.Services;
using backend.Infrastructure;
using backend.Infrastructure.Authentication;
using backend.Infrastructure.Repositories;
using backend.Infrastructure.Seed;
using backend.Infrastructure.Seed.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    var frontEndUrl = builder.Configuration.GetSection("AppSettings:FrontEndUrl").Value;
    if (frontEndUrl is null) throw new Exception("AppSettings:FrontEndUrl is not set in appSettings.json");
    options.AddDefaultPolicy(policy => policy.WithOrigins(frontEndUrl).AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("yu-gi-oh-postgres-database")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddLogging(configure => configure.AddConsole());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

// seeders
builder.Services.AddScoped<ISeedCommand, LocalizationSeed>();
builder.Services.AddScoped<ISeedCommand, CardSeed>();

// services
builder.Services.AddScoped<ProvinceService>();
builder.Services.AddScoped<MunicipalityService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<ArchetypeService>();
builder.Services.AddScoped<DeckService>();
builder.Services.AddScoped<TournamentsService>();
builder.Services.AddScoped<InscriptionService>();
builder.Services.AddScoped<DuelsService>();

// repositories
builder.Services.AddScoped<DbContext,AppDbContext>();
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
builder.Services.AddScoped<ICardRepository,CardRepository>();
builder.Services.AddScoped<IArchetypeRepository, ArchetypeRepository>();
builder.Services.AddScoped<IDeckRepository, DeckRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
