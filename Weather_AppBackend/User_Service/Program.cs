using System.Configuration;
using Common;
using Microsoft.Extensions.Configuration;
using Serilog;
using User_Service.Model;
using User_Service.Repository;
using User_Service.Service;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

// Add services to the container.

builder.Services.AddScoped<UserContext>(x => new UserContext(builder.Configuration));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logfiles\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
