using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace User_Service.Model
{
    public class UserDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]     
        public string? _id { get; private set; }

        [Required(ErrorMessage = "Userid is required")]
        [BsonElement("UserId")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "FullName is required")]
        [BsonElement("FullName")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [BsonElement("Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [BsonElement("Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [BsonElement("Email")]
        public string? Email { get; set; }


        [BsonElement("PhoneNumber")]
        public long? PhoneNumber { get; set; }

    }
}
