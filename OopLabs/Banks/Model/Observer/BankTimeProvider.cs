using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Banks.Model.Observer
{
    public class BankTimeProvider
    {
        public DateTime LastAccountsUpdate { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}