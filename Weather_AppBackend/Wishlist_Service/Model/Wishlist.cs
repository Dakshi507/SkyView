using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wishlist_Service.Model
{
    public class Wishlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; private set; }

        [Required(ErrorMessage = "Cityid is required")]
        [BsonElement("CityId")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "City is required")]
        [BsonElement("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [BsonElement("Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Country is required")]

        [BsonElement("Username")]
        public string Username { get; set; }


    }
}
