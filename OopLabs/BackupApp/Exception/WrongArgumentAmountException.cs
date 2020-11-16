using System;
using System.Collections.Generic;
using System.Text;

namespace BackupApp.Exception
{
    public class WrongArgumentAmountException : System.Exception
    {
        public WrongArgumentAmountException(int received, int expected) : base($"Wrong arguments amount! Received {received}, but expected {expected}.") {}
    }
}
