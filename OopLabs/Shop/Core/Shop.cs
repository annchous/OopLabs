using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
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
        public Dictionary<Product, ProductStatus> Products { get; set; }

        public Shop()
        {
            Id = 'S' + (++_counter).ToString();
            Products = new Dictionary<Product, ProductStatus>();
        }

        public Shop(string name, string address)
        {
            Id = 'S' + (++_counter).ToString();
            Name = name;
            Address = address;
            Products = new Dictionary<Product, ProductStatus>();
        }

        public Shop(string name, string address, Dictionary<Product, ProductStatus> products)
        {
            Id = 'S' + (++_counter).ToString();
            Name = name;
            Address = address;
            Products = new Dictionary<Product, ProductStatus>(products);
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
            return shop.Id == this.Id && shop.Name == this.Name && shop.Address == this.Address;
        }

        public void AddProduct(Product product) => Products.Add(product, new ProductStatus());
        public void AddProduct(Product product, ProductStatus productStatus) => Products.Add(product, productStatus);
        public void AddProduct(Product product, decimal price) => Products.Add(product, new ProductStatus(price));
        public void AddProduct(Product product, int amount) => Products.Add(product, new ProductStatus(amount));
        public void AddProduct(Product product, decimal price, int amount) => Products.Add(product, new ProductStatus(price, amount));

        public void AddProductLot(ProductLot lot)
        {
            foreach (var item in lot.Lot)
            {
                Products.Add(item.Key, item.Value);
            }
        }

        public void AddProductLot(Product product, ProductStatus productStatus) => Products.Add(product, productStatus);

        public void SetPrice(Product product, decimal price)
        {
            if (Products.FirstOrDefault(x => x.Key.Id == product.Id).Key.Equals(null))
                throw new ProductNotFound(ShopExceptionMessage.ProductNotFound);
            if (Products.FirstOrDefault(x => x.Key.Id == product.Id).Value.Equals(null))
                Products[Products.FirstOrDefault(x => x.Key.Id == product.Id).Key] = new ProductStatus();
            Products.FirstOrDefault(x => x.Key.Id == product.Id).Value.Price = price;
        }

        public object BuyLotOfProducts(ProductLot lot)
        {
            if (PossibleToBuyLot(lot))
            {
                decimal sum = 0.0m;
                foreach (var item in lot.Lot)
                {
                    var product = GetProductProductStatusPair(item);
                    product.Value.Value.Amount -= item.Value.Amount;
                    sum += item.Value.Amount * product.Value.Value.Price;
                }

                return sum;
            }
            return ShopExceptionMessage.ImpossibleToBuyLot;
        }

        public bool PossibleToBuyLot(ProductLot lot)
        {
            foreach (var item in lot.Lot)
            {
                var product = GetProductProductStatusPair(item);
                if (product.Equals(null))
                    return false;
            }

            return true;
        }

        public KeyValuePair<Product, ProductStatus>? GetProductProductStatusPair(KeyValuePair<Product, ProductStatus> pair) 
            => Products.Where(x => x.Key.Id == pair.Key.Id
                && x.Key.Name == pair.Key.Name
                && x.Value.Amount >= pair.Value.Amount)
                .Select(x => (KeyValuePair<Product, ProductStatus>?) x)
                .FirstOrDefault();

        public Dictionary<Product, ProductStatus> GetProductsOnSum(decimal price)
        {
            Dictionary<Product, ProductStatus> result = new Dictionary<Product, ProductStatus>();
            var availableToBuy = from product in Products
                where product.Value.Amount > 0 && product.Value.Price <= price
                select product;
            if (availableToBuy.Equals(null))
                throw new ImpossibleToBuy(ShopExceptionMessage.ImpossibleToBuy + $"{price} in the shop {Name}!");

            foreach (var item in availableToBuy)
            {
                Product product = (Product) item.Key.Clone();
                ProductStatus productStatus = (ProductStatus) item.Value.Clone();
                result.Add(product, productStatus);
                var availableAmount = (int) (price / productStatus.Price);
                if (availableAmount <= productStatus.Amount)
                    result[product].Amount = availableAmount;
            }

            return result;
        }

        public object Clone()
        {
            Dictionary<Product, ProductStatus> products = new Dictionary<Product, ProductStatus>(this.Products);
            return new Shop {Name = this.Name, Address = this.Address, Products = products};
        }
    }
}