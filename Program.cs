using LearningWebApi.Middlewares;
using LearningWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders()
    .AddConsole()
    .AddDebug();

builder.Services.AddServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExeptionMiddleware();
}

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