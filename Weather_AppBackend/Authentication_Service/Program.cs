using Authentication_Service.Model;
using Authentication_Service.Repository;
using Authentication_Service.Service;
using Consul;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Common;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logfiles\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherAppdb"));
});
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddConsulConfig(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x => x.AllowAnyOrigin().
                 AllowAnyMethod().
                 AllowAnyHeader());



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseConsul(configuration);

app.MapControllers();

app.Run();
