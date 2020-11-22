using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banks.Exceptions
{
    public class NotVerifiedClientException : Exception
    {
        public NotVerifiedClientException(Guid clientId) : base($"Client with {clientId} ID is not verified.") {}
    }
}
