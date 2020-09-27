using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core
{
    class ProductRequest : ICloneable
    {
        public Product Product { get; set; }
        public ProductStatus ProductStatus { get; set; }

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

        public object Clone()
        {
            return new ProductRequest 
                {Product = (Product)this.Product.Clone(), 
                    ProductStatus = (ProductStatus)this.ProductStatus};
        }
    }
}
