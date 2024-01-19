namespace User_Service.Exceptions
{
    public class UsernameAlreadyExistException : ApplicationException
    {
        public UsernameAlreadyExistException(){ }
        public UsernameAlreadyExistException(string message) : base(message) { }
    }
}
