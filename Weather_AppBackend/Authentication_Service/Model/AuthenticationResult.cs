namespace Authentication_Service.Model
{
    public class AuthenticationResult
    {
        public string message { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string TokenValue { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
