using System;
using System.Collections.Generic;

namespace Shop
{
    class ProductStatus
    {
        public decimal Price { get; }
        public int Amount { get; }

        public ProductStatus()
            :this(0, 0)
        {}

        public ProductStatus(decimal price)
            : this(price, 0)
        {}
        public ProductStatus(int amount)
            : this(0, amount)
        {}
        public ProductStatus(decimal price, int amount)
        {
            Price = price;
            Amount = amount;
        }

        public ProductStatus CopyWith(decimal price, int amount) => new ProductStatus(price, amount);
    }
}