using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Exception
{
    namespace ShopException
    {
        public class ProductNotFoundException : System.Exception
        {
            public ProductNotFoundException(string message = "Product was not found!")
                : base(message)
            { }
        }
        public class ShopNotFoundException : System.Exception
        {
            public ShopNotFoundException(string message = "Shop was not found!")
                : base(message)
            { }
        }

        public class ImpossibleToBuyException : System.Exception
        {
            public ImpossibleToBuyException(string message = "Impossible to buy!")
                : base(message)
            {}
        }

        public class ImpossibleToBuyLotException : System.Exception
        {
            public ImpossibleToBuyLotException(string message = "Impossible to buy this lot!")
                : base(message)
            { }
        }
    }
}
