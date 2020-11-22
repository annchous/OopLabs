using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banks.Exceptions
{
    class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(Guid accountId) : base($"Account with {accountId} ID was not found.") {}
    }
}
