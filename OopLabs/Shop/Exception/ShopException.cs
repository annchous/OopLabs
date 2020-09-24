using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Exception
{
    namespace ShopException
    {
        public class ProductNotFound : System.Exception
        {
            public ProductNotFound(string message)
                : base(message)
            { }
        }
        public class ShopNotFound : System.Exception
        {
            public ShopNotFound(string message)
                : base(message)
            { }
        }

        public class ImpossibleToBuy : System.Exception
        {
            public ImpossibleToBuy(string message)
                : base(message)
            {}
        }
    }
}
