using System.Text;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    var frontEndUrl = builder.Configuration.GetSection("AppSettings:FrontEndUrl").Value;
    if (frontEndUrl is null) throw new Exception("AppSettings:FrontEndUrl is not set in appSettings.json");
    options.AddDefaultPolicy(policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
    if (jwtOptions is null) throw new Exception("The Jwt settings are not set in appSettings");
    
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidIssuer = jwtOptions.Issuer,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
    };
});
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


builder.Services.AddAuthorization();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

// seeders
builder.Services.AddScoped<ISeedCommand, LocalizationSeed>();
builder.Services.AddScoped<ISeedCommand, CardSeed>();
builder.Services.AddScoped<ISeedCommand, RolesAndClaimsSeed>();

// services
builder.Services.AddScoped<ProvinceService>();
builder.Services.AddScoped<MunicipalityService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<ArchetypeService>();
builder.Services.AddScoped<DeckService>();
builder.Services.AddScoped<TournamentsService>();
builder.Services.AddScoped<InscriptionService>();
builder.Services.AddScoped<DuelsService>();
builder.Services.AddScoped<UserService>();

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
