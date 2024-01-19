namespace User_Service.Exceptions
{
    public class UserIdAlredyExistexception : ApplicationException
    {
        public UserIdAlredyExistexception() { }
        public UserIdAlredyExistexception(string message) : base(message) { }

    }
}
