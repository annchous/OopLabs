using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Core;
using Shop.Exception.ShopException;

namespace Shop
{
    class Shop
    {
        private static int _counter;
        private readonly int _id;
        public string Id { get; }
        public string Name { get; }
        public string Address { get; }
        public List<ProductRequest> Products { get; }

        public Shop(string name, string address)
            : this(name, address, new List<ProductRequest>())
        {}

        public Shop(string name, string address, List<ProductRequest> products)
        {
            _id = ++_counter;
            Id = 'S' + _id.ToString();
            Name = name;
            Address = address;
            Products = new List<ProductRequest>(products);
        }

        private Shop(string id, string name, string address, List<ProductRequest> products)
        {
            Id = id;
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
        public void AddProduct(Product product, decimal price, int amount) 
            => Products.Add(new ProductRequest(product, new ProductStatus(price, amount)));

        public void AddProductLot(ProductLot lot) => Products.AddRange(lot.Lot);
        public void AddProductLot(Product product, ProductStatus productStatus) => Products.Add(new ProductRequest(product, productStatus));

        public void SetPrice(Product product, decimal price)
        {
            var productToSet = 
                Products.FirstOrDefault(x => x.Product.Id == product.Id) 
                ?? throw new ProductNotFoundException();

            Products[Products.IndexOf(productToSet)] = productToSet.CopyWith(
                productToSet.Product, new ProductStatus(price, productToSet.ProductStatus.Amount));
        }

        public void SetAmount(Product product, int amount)
        {
            var productToSet =
                Products.FirstOrDefault(x => x.Product.Id == product.Id)
                ?? throw new ProductNotFoundException();

            Products[Products.IndexOf(productToSet)] = productToSet.CopyWith(
                productToSet.Product, new ProductStatus(productToSet.ProductStatus.Price, amount));
        }

        public decimal BuyLotOfProducts(ProductLot lot) => TryBuyLot(lot, out decimal sum)
            ? sum
            : throw new ImpossibleToBuyLotException();

        public bool TryBuyLot(ProductLot lot, out decimal sum)
        {
            sum = 0.0m;

            foreach (var item in lot.Lot)
            {
                if (FindProductRequest(item) == null)
                    return false;
            }

            foreach (var item in lot.Lot)
            {
                var product = FindProductRequest(item);
                var newAmount = product.ProductStatus.Amount - item.ProductStatus.Amount;
                Products[Products.IndexOf(product)] = product.CopyWith(product.Product, 
                    product.ProductStatus.CopyWith(product.ProductStatus.Price, newAmount));
                sum += item.ProductStatus.Amount * product.ProductStatus.Price;
            }

            return true;
        }

        public ProductRequest FindProductRequest(ProductRequest pair) => 
            Products.FirstOrDefault(x =>
                    x.Product.Id == pair.Product.Id
                    && x.Product.Name == pair.Product.Name
                    && x.ProductStatus.Amount >= pair.ProductStatus.Amount);

        public List<ProductRequest> GetProductsOnSum(decimal price)
        {
            List<ProductRequest> result = new List<ProductRequest>();
            var availableToBuy = GetAvailableToBuyList(price);
            if (availableToBuy.Count == 0) throw new ImpossibleToBuyException();

            foreach (var item in availableToBuy)
            {
                ProductRequest productRequest = new ProductRequest(item.Product, item.ProductStatus);

                result.Add(productRequest);
                var availableAmount = (int)(price / productRequest.ProductStatus.Price);
                if (availableAmount <= productRequest.ProductStatus.Amount)
                {
                    var product = result[result.IndexOf(productRequest)];
                    result[result.IndexOf(productRequest)] = product.CopyWith(product.Product,
                        product.ProductStatus.CopyWith(product.ProductStatus.Price, availableAmount));
                }
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

        public Shop CopyWith(string id, string name, string address, List<ProductRequest> products) 
            => new Shop(id, name, address, products);

        public void PrintShop() => ShopPrinter.PrintSingleShop(this);
    }
}