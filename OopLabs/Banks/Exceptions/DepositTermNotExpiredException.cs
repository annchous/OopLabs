using System;

namespace Banks.Exceptions
{
    public class DepositTermNotExpiredException : Exception
    {
        public DepositTermNotExpiredException() : base("The term of the deposit has not expired.") {}
    }
}