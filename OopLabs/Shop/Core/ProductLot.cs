using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop
{
    class ProductLot
    {
        public Dictionary<Product, ProductStatus> Lot { get; set; }

        public ProductLot()
        {
            Lot = new Dictionary<Product, ProductStatus>();
        }

        public ProductLot(Product product)
            : this(product, 0, 0)
        {}

        public ProductLot(Product product, int amount)
            : this(product, 0, amount)
        {}
        public ProductLot(Product product, decimal price)
            : this(product, price, 0)
        {}

        public ProductLot(Product product, decimal price, int amount)
        {
            Lot = new Dictionary<Product, ProductStatus>
            {
                { product, new ProductStatus(price, amount) }
            };
        }

        public ProductLot(Dictionary<Product, ProductStatus> lot)
        {
            Lot = new Dictionary<Product, ProductStatus>(lot);
        }

        public void SetPrice(string id, decimal price) => GetProduct(id).Value.Price = price;
        public void SetPrice(Product product, decimal price) => GetProduct(product.Id).Value.Price = price;
        public KeyValuePair<Product, ProductStatus> GetProduct(string id) 
            => Lot.FirstOrDefault(x => x.Key.Id == id);

        public void BringToShop(Shop shop)
        {
            foreach (var item in Lot)
            {
                shop.AddProduct(item.Key, item.Value);
            }
        }
    }
}