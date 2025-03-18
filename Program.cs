using LearningWebApi.AutoMapperProfiles;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.EntityModels;
using LearningWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

builder.Services.AddControllers(option => option.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<WebApiDataBaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

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

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LoginPath = "/Account/Login";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseHsts();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Redirecting from: {context.Request.Path}");
    await next();
});



app.MapControllers();

app.Run();


