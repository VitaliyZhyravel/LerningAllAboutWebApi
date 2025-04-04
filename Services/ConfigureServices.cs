using LearningWebApi.AutoMapperProfiles;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.EntityModels;
using LearningWebApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace LearningWebApi.Services
{
    static public class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddControllers(option => option.SuppressAsyncSuffixInActionNames = true);

            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddScoped<IJwtToken, JwtToken>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddDbContext<WebApiDataBaseContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Some-Token"];

                            return Task.CompletedTask;
                        }
                    };

                });

            services.AddAuthorization();

            services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
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

            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
