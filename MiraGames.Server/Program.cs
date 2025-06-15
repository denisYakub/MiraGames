using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiraGames.Server.Database.Contexts;
using MiraGames.Server.Database.Repositories;
using MiraGames.Server.Database.Scripts;
using MiraGames.Server.Entities;
using MiraGames.Server.Interfaces;
using MiraGames.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<PostgresContext>(options => {
        var connection = builder.Configuration.GetConnectionString("PostgresConnection");

        options.UseNpgsql(connection);
    });

builder.Services
    .AddAuthentication(options => 
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwt["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"])
            ),
            ValidateLifetime = true
        };
    });

builder.Services
    .AddScoped<ITokenService<User>, TokenService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IRateService, RateService>()
    .AddScoped<IPaymentService, PaymentService>();

builder.Services
    .AddScoped<IRepository<User>, UserRepository>()
    .AddScoped<IRepository<Rate>, RateRepository>()
    .AddScoped<IRepository<Payment>, PaymentRepository>();

builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson(option =>
        option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

InitPgScript.Init(app.Services);

app.Run();
