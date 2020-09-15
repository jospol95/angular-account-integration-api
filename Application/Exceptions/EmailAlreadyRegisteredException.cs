using System;

namespace AuthorizationAPI.Application.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        
        public EmailAlreadyRegisteredException()
            :base("Incorrect email or password")
        {
        }
        
        public EmailAlreadyRegisteredException(string message)
            :base(message)
        {
        }
    }
}