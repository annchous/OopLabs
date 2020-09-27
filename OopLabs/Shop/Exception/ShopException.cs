using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Exception
{
    namespace ShopException
    {
        public class ProductNotFoundException : System.Exception
        {
            public ProductNotFoundException(string message)
                : base(message)
            { }
        }
        public class ShopNotFoundException : System.Exception
        {
            public ShopNotFoundException(string message)
                : base(message)
            { }
        }

        public class ImpossibleToBuyException : System.Exception
        {
            public ImpossibleToBuyException(string message = "Impossible to buy this lot!")
                : base(message)
            {}
        }
    }
}
