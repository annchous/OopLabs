using System;

namespace Banks.Exceptions
{
    public class ClientAlreadyExistsException : Exception
    {
        public ClientAlreadyExistsException() : base("Client with such personal data already exists.") 
        {}
    }
}