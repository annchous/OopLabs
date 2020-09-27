using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Shop.Core;
using Shop.Exception;
using Shop.Exception.ShopException;
using Spectre.Console;

namespace Shop
{
    class ShopList
    {
        public List<Shop> Shops { get; set; }

        public ShopList()
        {
            Shops = new List<Shop>();
        }

        public ShopList(Shop shop)
        {
            Shops = new List<Shop>
            {
                shop
            };
        }

        public ShopList(List<Shop> shops)
        {
            Shops = new List<Shop>();
            Shops = shops.GetRange(0, shops.Count);
        }

        public Shop GetShopWithLowestPriceOn(string id)
        {
            var prices = GetPricesOn(id)
                ?? throw new ProductNotFoundException(ShopExceptionMessage.ProductNotFound);
            decimal? minPrice = prices.Min();
            var result = 
                (from shop in Shops
                from product in shop.Products
                where product.Product.Id == id && product.ProductStatus.Price == minPrice
                select shop).FirstOrDefault()
                ?? throw new ShopNotFoundException(ShopExceptionMessage.ShopNotFound);
            return result;
        }

        public IEnumerable<decimal> GetPricesOn(string id) =>
            from shop in Shops
            from product in shop.Products
            where product.Product.Id == id && product.ProductStatus.Amount > 0
            select product.ProductStatus.Price;

        public Shop GetShopWithLowestSumOnLot(ProductLot lot)
        {
            decimal minSum = Decimal.MaxValue;
            Shop shopWithMinSum = null;
            foreach (var shop in Shops)
            {
                if (TryGetSumOnLot(shop, lot, out decimal sum))
                    if (sum < minSum)
                    {
                        minSum = sum;
                        shopWithMinSum = shop;
                    }
            }

            return shopWithMinSum 
                   ?? throw new ShopNotFoundException(
                       ShopExceptionMessage.ShopNotFound);
        }

        public bool TryGetSumOnLot(Shop shop, ProductLot productLot, out decimal result)
        {
            result = 0.0m;
            if (shop.TryGetSumOnLot(productLot, out decimal sum))
            {
                result = sum;
                return true;
            }

            return false;
        }

        public void PrintShops() => ShopPrinter.PrintShopList(this);
    }
}
