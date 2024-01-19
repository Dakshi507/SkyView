using Serilog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User_Service.Exceptions;
using Wishlist_Service.Exceptions;
using Wishlist_Service.Model;
using Wishlist_Service.Service;

namespace Wishlist_Service.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishlistService wishListService;

        public WishListController(IWishlistService _wishListService)
        {
            wishListService = _wishListService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCityinWishList([FromBody] Wishlist wishlist)
        {
            try
            {
                var createdUser = await wishListService.CreateCity(wishlist);
                Log.Information("City added to wishlist: {CityName}", wishlist.City);
                var responseData = new { Message = "City Added to your Wishlist", CityName = wishlist.City };
                return Ok(responseData);
            }
            catch (AlredyExistexception ex)
            {
                Log.Warning("City already exists in wishlist: {CityName}", wishlist.City);
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding city to wishlist");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAlCity()
        {
            try
            {
                var city = await wishListService.GetAllCity();
                Log.Information("Retrieved all cities from wishlist");
                return Ok(city);
            }
            catch (NotFoundException ex)
            {
                Log.Warning("No cities found in wishlist");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting all cities from wishlist");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

/*        [HttpGet("city/{Cityname}")]
        public async Task<IActionResult> GetCitybyName(string Cityname)
        {
            try
            {
                var city = await wishListService.GetCitybyName(Cityname);
                Log.Information("Retrieved city by name: {CityName}", Cityname);
                return Ok(city);
            }
            catch (NotFoundException ex)
            {
                Log.Warning("City not found by name: {CityName}", Cityname);
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting city by name: {CityName}", Cityname);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }*/

        [HttpGet("{username}")]
        public async Task<IActionResult> GetCitybyUsername(string username)
        {
            try
            {
                var city = await wishListService.GetCitybyUsername(username);
                Log.Information("Retrieved city by user ID: {username}", username);
                return Ok(city);
            }
            catch (NotFoundException ex)
            {
                Log.Warning("No cities found for user ID: {username}", username);
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting city by user ID: {username}", username);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("city/{cityName}/user/{username}")]
        public async Task<IActionResult> DeleteCity(string cityName, string username)
        {
            try
            {
                await wishListService.DeleteCity(cityName, username);
                Log.Information("City removed from wishlist: {CityName}", cityName);
                var responseData = new { Message = "City removed from Wishlist", CityName = cityName };

               
                return Ok(responseData);
            }
            catch (NotFoundException ex)
            {
                Log.Warning("City not found for deletion: {CityName}", cityName);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting city from wishlist: {CityName}", cityName);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
