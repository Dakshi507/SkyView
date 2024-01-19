using Wishlist_Service.Model;

namespace Wishlist_Service.Repository
{
    public interface IWishlistRepository
    {
        Task CreateWishlist(Wishlist wishlist);
        Task<bool> ExistsCityForUser(string city, string username);
        Task<bool> ExistsCityId(int cityId);
        Task<List<Wishlist>> GetAllCity();
        Task<Wishlist> GetCitybyName(string city);
        Task<List<Wishlist>> GetCitybyUserName(string userName);
        Task DeleteCity(string city, string username);

    }
}
