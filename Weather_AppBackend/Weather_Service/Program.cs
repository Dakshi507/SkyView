using Serilog;
using Common;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logfiles\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();


// Add services to the container.

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
