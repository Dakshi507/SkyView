using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_Service.Model
{
    public class JwtToken
    {
        [Key]
        public int TokenId { get; set; }
        public string TokenValue { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? LastLogin { get; set; }

        public int UserId { get; set; } 

      

    }
}
