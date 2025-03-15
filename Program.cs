using LearningWebApi.AutoMapperProfiles;
using LearningWebApi.DataBaseContext;
using LearningWebApi.Models.EntityModels;
using LearningWebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

builder.Services.AddControllers(option => option.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddControllers();

builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<WebApiDataBaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()

    .AddEntityFrameworkStores<WebApiDataBaseContext>()
    .AddDefaultTokenProviders();
   

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


