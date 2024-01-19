using Serilog;
using Wishlist_Service.Model;
using Wishlist_Service.Repository;
using Wishlist_Service.Service;
using Common;


var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logfiles\\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


// Add services to the container.
builder.Services.AddScoped<WishListContext>(x => new WishListContext(builder.Configuration));

builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IWishlistService, WishlistService>();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
