using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Schema;
using Shop.Core;
using Shop.Exception;
using Shop.Exception.ShopException;
using Spectre.Console;

namespace Shop
{
    class Shop : ICloneable
    {
        private static int _counter;
        public string Id { get; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<ProductRequest> Products { get; set; }

        public Shop()
        {
            Id = 'S' + (++_counter).ToString();
            Products = new List<ProductRequest>();
        }

        public Shop(string name, string address)
        {
            Id = 'S' + (++_counter).ToString();
            Name = name;
            Address = address;
            Products = new List<ProductRequest>();
        }

        public Shop(string name, string address, List<ProductRequest> products)
        {
            Id = 'S' + (++_counter).ToString();
            Name = name;
            Address = address;
            Products = new List<ProductRequest>(products);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Shop shop = obj as Shop;
            if (shop == null) return false;
            return shop.Id == this.Id 
                   && shop.Name == this.Name 
                   && shop.Address == this.Address;
        }

        public void AddProduct(Product product) => Products.Add(new ProductRequest(product, new ProductStatus()));
        public void AddProduct(Product product, ProductStatus productStatus) => Products.Add(new ProductRequest(product, productStatus));
        public void AddProduct(Product product, decimal price) => Products.Add(new ProductRequest(product, new ProductStatus(price)));
        public void AddProduct(Product product, int amount) => Products.Add(new ProductRequest(product, new ProductStatus(amount)));
        public void AddProduct(Product product, decimal price, int amount) => Products.Add(new ProductRequest(product, new ProductStatus(price, amount)));

        public void AddProductLot(ProductLot lot)
        {
            foreach (var item in lot.Lot)
            {
                Products.Add(new ProductRequest(item.Product, item.ProductStatus));
            }
        }

        public void AddProductLot(Product product, ProductStatus productStatus) => Products.Add(new ProductRequest(product, productStatus));

        public void SetPrice(Product product, decimal price)
        {
            var productToSet = 
                Products.FirstOrDefault(x => x.Product.Id == product.Id) 
                ?? throw new ProductNotFoundException(ShopExceptionMessage.ProductNotFound);

            productToSet.ProductStatus ??= new ProductStatus();
            productToSet.ProductStatus.Price = price;
        }

        public bool EmptyShop() => Products.Count == 0;

        public decimal BuyLotOfProducts(ProductLot lot) => TryBuyLot(lot, out decimal sum)
            ? sum
            : throw new ImpossibleToBuyException();

        public bool TryBuyLot(ProductLot lot, out decimal sum)
        {
            sum = 0.0m;

            foreach (var item in lot.Lot)
            {
                if (GetProductRequest(item) == null)
                    return false;
            }

            foreach (var item in lot.Lot)
            {
                var product = GetProductRequest(item).ProductStatus;
                product.Amount -= item.ProductStatus.Amount;
                sum += item.ProductStatus.Amount * product.Price;
            }

            return true;
        }

        public ProductRequest GetProductRequest(ProductRequest pair) => 
            Products.FirstOrDefault(x =>
                    x.Product.Id == pair.Product.Id
                    && x.Product.Name == pair.Product.Name
                    && x.ProductStatus.Amount >= pair.ProductStatus.Amount);

        public List<ProductRequest> GetProductsOnSum(decimal price)
        {
            List<ProductRequest> result = new List<ProductRequest>();
            var availableToBuy = GetAvailableToBuyList(price)
                ?? throw new ImpossibleToBuyException();

            foreach (var item in availableToBuy)
            {
                ProductRequest productRequest = new ProductRequest(
                    (Product)item.Product.Clone(), 
                    (ProductStatus)item.ProductStatus.Clone());

                result.Add(productRequest);
                var availableAmount = (int)(price / productRequest.ProductStatus.Price);
                if (availableAmount <= productRequest.ProductStatus.Amount)
                    result[result.IndexOf(productRequest)].ProductStatus.Amount = availableAmount;
            }

            return result;
        }

        public List<ProductRequest> GetAvailableToBuyList(decimal price) => Products
            .Select(x => x)
            .Where(x => 
                x.ProductStatus.Amount > 0 
                && x.ProductStatus.Price <= price)
            .ToList();

        public bool TryGetSumOnLot(ProductLot productLot, out decimal sum)
        {
            sum = 0.0m;

            foreach (var item in productLot.Lot)
            {
                if (TryGetProduct(item, out ProductRequest product))
                    sum += product.ProductStatus.Price * item.ProductStatus.Amount;
                else return false;

            }

            return true;
        }

        public bool TryGetProduct(ProductRequest productRequest, out ProductRequest product)
        {
            product = Products
                .Where(p =>
                    p.Product.Id == productRequest.Product.Id &&
                    p.Product.Name == productRequest.Product.Name &&
                    p.ProductStatus.Amount >= productRequest.ProductStatus.Amount)
                .Select(p => p)
                .FirstOrDefault();
            return product != null;
        }

        public object Clone()
        {
            List<ProductRequest> products = new List<ProductRequest>(this.Products);
            return new Shop { Name = this.Name, Address = this.Address, Products = products };
        }

        public void PrintShop() => ShopPrinter.PrintSingleShop(this);
    }
}