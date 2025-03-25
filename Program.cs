using LearningWebApi.AutoMapperProfiles;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.EntityModels;
using LearningWebApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

builder.Services.AddControllers(option => option.SuppressAsyncSuffixInActionNames = true);

builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddScoped<IJwtToken, JwtToken>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<WebApiDataBaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["cokies"];
             
                return Task.CompletedTask;
            }

        };

    });

builder.Services.AddAuthorization();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireDigit = true;
    option.Password.RequiredUniqueChars = 3;
})
    .AddEntityFrameworkStores<WebApiDataBaseContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

//DESKTOP-710MH0F
//LAPTOP-38CA3JVD