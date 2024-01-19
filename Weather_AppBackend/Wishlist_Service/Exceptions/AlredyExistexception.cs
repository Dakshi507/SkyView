namespace User_Service.Exceptions
{
    public class AlredyExistexception : ApplicationException
    {
        public AlredyExistexception() { }
        public AlredyExistexception(string message) : base(message) { }

    }
}
