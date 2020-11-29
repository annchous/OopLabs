using System;

namespace Banks.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Insufficient funds on the balance sheet to complete the transaction.") 
        {}
    }
}