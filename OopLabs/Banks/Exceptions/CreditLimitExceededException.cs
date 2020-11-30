using System;

namespace Banks.Exceptions
{
    public class CreditLimitExceededException : Exception
    {
        public CreditLimitExceededException() : base("Credit limit exceeded.") 
        {}
    }
}