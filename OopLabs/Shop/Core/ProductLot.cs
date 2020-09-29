using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.Core;

namespace Shop
{
    class ProductLot
    {
        public List<ProductRequest> Lot { get; set; }

        public ProductLot()
        {
            Lot = new List<ProductRequest>();
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
            Lot = new List<ProductRequest>
            {
                new ProductRequest(product, new ProductStatus(price, amount))
            };
        }

        public ProductLot(List<ProductRequest> lot)
        {
            Lot = new List<ProductRequest>(lot);
        }

        public void AddToLot(ProductRequest productRequest) => Lot.Add(productRequest);
        public void AddToLot(Product product, ProductStatus productStatus) => Lot.Add(new ProductRequest(product, productStatus));
        public void AddToLot(Product product) => Lot.Add(new ProductRequest(product, new ProductStatus()));
        public void AddToLot(Product product, decimal price, int amount) => Lot.Add(new ProductRequest(product, new ProductStatus(price, amount)));
        public void AddToLot(Product product, decimal price) => Lot.Add(new ProductRequest(product, new ProductStatus(price)));
        public void AddToLot(Product product, int amount) => Lot.Add(new ProductRequest(product, new ProductStatus(amount)));

        public void SetPrice(string id, decimal price) => GetProduct(id).ProductStatus.Price = price;
        public void SetPrice(Product product, decimal price) => GetProduct(product.Id).ProductStatus.Price = price;
        public ProductRequest GetProduct(string id) 
            => Lot.FirstOrDefault(x => x.Product.Id == id);

        public void BringToShop(Shop shop)
        {
            foreach (var item in Lot)
            {
                shop.AddProduct(item.Product, item.ProductStatus);
            }
        }
    }
}