using System;
using System.Collections.Generic;

namespace Shop.Core
{
    class ProductRequest
    {
        public Product Product { get; }
        public ProductStatus ProductStatus { get; }

        public ProductRequest()
            : this(new Product(), new ProductStatus())
        {}
        public ProductRequest(Product product)
            : this(product, new ProductStatus())
        {}
        public ProductRequest(Product product, ProductStatus productStatus)
        {
            Product = product;
            ProductStatus = productStatus;
        }

        public override int GetHashCode()
        {
            return Product.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            ProductRequest productRequest = obj as ProductRequest;

            if (productRequest == null) return false;
            return this.Product == productRequest.Product 
                   && this.ProductStatus == productRequest.ProductStatus;
        }
        
        public ProductRequest CopyWith(Product product, ProductStatus productStatus) 
            => new ProductRequest(
                product.CopyWith(product.Id, product.Name), 
                productStatus.CopyWith(productStatus.Price, productStatus.Amount));
    }
}
