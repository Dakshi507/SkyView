using Wishlist_Service.Model;

namespace Wishlist_Service.Service
{
    public interface IWishlistService
    {
        Task<Wishlist> CreateCity(Wishlist wishlist);
        Task<List<Wishlist>> GetAllCity();
        Task<Wishlist> GetCitybyName(string city);
        Task<List<Wishlist>> GetCitybyUsername(string userName);
        Task DeleteCity(string city, string username);
    }
}
