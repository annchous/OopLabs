using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Core;

namespace Shop
{
    class ProductLot
    {
        public List<ProductRequest> Lot { get; }

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

        public void SetPrice(string id, decimal price)
        {
            var productToSet = FindProduct(id);
            productToSet = productToSet.CopyWith(
                productToSet.Product.CopyWith(productToSet.Product.Id, productToSet.Product.Name),
                productToSet.ProductStatus.CopyWith(price, productToSet.ProductStatus.Amount));
            Lot[Lot.IndexOf(productToSet)] = productToSet;
        }
        public void SetPrice(Product product, decimal price)
        {
            var productToSet = FindProduct(product.Id);
            productToSet = productToSet.CopyWith(
                productToSet.Product,
                productToSet.ProductStatus.CopyWith(price, productToSet.ProductStatus.Amount));
            Lot[Lot.IndexOf(productToSet)] = productToSet;
        }
        public ProductRequest FindProduct(string id) 
            => Lot.FirstOrDefault(x => x.Product.Id == id);

    }
}