namespace Wishlist_Service.Exceptions
{
    public class CitynotAddedException :ApplicationException
    {
        public CitynotAddedException(string v) { }
        public CitynotAddedException(string message, Exception ex) : base(message) { }
    }
}
