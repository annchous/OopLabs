using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
    class ProductStatus : ICloneable
    {
        public decimal Price { get; set; }
        public int Amount { get; set; }

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

        public object Clone()
        {
            return new ProductStatus {Price = this.Price, Amount = this.Amount};
        }
    }
}