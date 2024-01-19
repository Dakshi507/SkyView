using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Weather_Service.Model;


namespace Weather_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetWeatherByCityName(string cityName)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"weather?q={cityName}&appid=d9b0b32c2fb06df805567b1ff0c45f16&units=metric");

                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
                    Log.Information("Weather data fetched successfully for {CityName}", cityName);
                    return Ok(weatherData);
                }
                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);
                    Log.Warning("Error fetching weather data for {CityName}: {ErrorMessage}", cityName, errorResponse.Message);
                    return NotFound(errorResponse);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching weather data for {CityName}", cityName);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
