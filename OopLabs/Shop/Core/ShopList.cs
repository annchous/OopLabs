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

        public Shop GetShopWithLowestPriceOn(string id) => TryGetShop(id, out Shop shop) 
            ? shop 
            : throw new ShopNotFoundException();

        public Shop GetShopWithLowestPriceOn(Product product) => GetShopWithLowestPriceOn(product.Id);

        public bool TryGetShop(string id, out Shop shop)
        {
            shop = (from _shop in Shops
                from product in _shop.Products
                where product.Product.Id == id && product.ProductStatus.Amount > 0
                orderby product.ProductStatus.Price
                select _shop).FirstOrDefault();
            return shop != null;
        }

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
                   ?? throw new ShopNotFoundException();
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
