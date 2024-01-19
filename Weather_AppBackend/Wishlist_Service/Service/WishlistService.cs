using User_Service.Exceptions;
using Wishlist_Service.Exceptions;
using Wishlist_Service.Model;
using Wishlist_Service.Repository;

namespace Wishlist_Service.Service
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository wishListRepo;
        public WishlistService(IWishlistRepository _userRepo)
        {
            wishListRepo = _userRepo;
        }

        public async Task<Wishlist> CreateCity(Wishlist wishlist)
        {
            var existcity = await wishListRepo.ExistsCityForUser(wishlist.City, wishlist.Username);
            var existcityId = await wishListRepo.ExistsCityId(wishlist.CityId);
            if (existcity)
            {
                throw new AlredyExistexception("city already exists in wishlist");
            }
            else if (existcityId)
            {
                throw new AlredyExistexception("UserId already exists");
            }
            else
            {
                try
                {
                    await wishListRepo.CreateWishlist(wishlist);
                    return wishlist;
                }
                catch (Exception ex)
                {
                    throw new CitynotAddedException("Failed to Add in wishlist", ex);
                }
            }

        }

        public async Task DeleteCity(string city, string username)
        {
            try
            {
                var cityExists = await wishListRepo.ExistsCityForUser(city, username);

                if (!cityExists)
                {
                    throw new NotFoundException("City not found for deletion");
                }

                await wishListRepo.DeleteCity(city, username);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete city", ex);
            }
        }

        public async Task<List<Wishlist>> GetAllCity()
        {
            var wishlist = await wishListRepo.GetAllCity();
            if (wishlist == null)
            {
                throw new NotFoundException("WishList is empty ");
            }
            else
            {
                try
                {
                    return wishlist;
                }
                catch (Exception ex)
                {
                    throw new Exception("some exception error", ex);
                }
            }
            
        }

        public async Task<Wishlist> GetCitybyName(string city)
        {
            var wishlist = await wishListRepo.GetCitybyName(city);
            if (wishlist == null)
            {
                throw new NotFoundException("City not found ");
            }
            else
            {
                try
                {
                    return wishlist;
                }
                catch (Exception ex)
                {
                    throw new Exception("some exception error", ex);
                }
            }
        }

        public async Task<List<Wishlist>> GetCitybyUsername(string username)
        {
            var wishlist = await wishListRepo.GetCitybyUserName(username);
            if (wishlist == null)
            {
                throw new NotFoundException("User not found ");
            }
            else
            {
                try
                {
                    return wishlist;
                }
                catch (Exception ex)
                {
                    throw new Exception("some exception error", ex);
                }
            }
        }
    }
}
