using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            var prices = GetPricesOn(id);
            // TODO: Custom exception for null
            decimal? minPrice = prices.Min();
            var ans = 
                from shop in Shops
                from product in shop.Products
                where product.Key.Id == id && product.Value.Price == minPrice
                select shop;
            // TODO: Custom exception for null
            return ans.FirstOrDefault();
        }
        public IEnumerable<decimal> GetPricesOn(string id) => 
            from shop in Shops
            from product in shop.Products
            where product.Key.Id == id && product.Value.Amount > 0
            select product.Value.Price;

        public Dictionary<Shop, Dictionary<Product, ProductStatus>> GetProductsOnSum(decimal price)
        {
            Dictionary<Shop, Dictionary<Product, ProductStatus>> productList = new Dictionary<Shop, Dictionary<Product, ProductStatus>>();
            foreach (var shop in Shops)
            {
                productList.Add((Shop)shop.Clone(), new Dictionary<Product, ProductStatus>(shop.GetProductsOnSum(price)));
            }

            return productList;
        }

        public void PrintShops()
        {
            var table = new Table();
            CreateTableHeader(table);
            foreach (var shop in Shops)
            {
                foreach (var item in shop.Products)
                {
                    table.AddRow($"[red]{shop.Id}[/]", $"[white]{shop.Name}[/]", 
                        $"[aqua]{item.Key.Id}[/]", $"[white]{item.Key.Name}[/]", 
                        $"[green]{item.Value.Price}[/]", $"[yellow]{item.Value.Amount}[/]");
                }
            }
            AnsiConsole.Render(table);
        }

        public void CreateTableHeader(Table table)
        {
            table.AddColumn(new TableColumn("[u]Id магазина[/]"));
            table.AddColumn(new TableColumn("[u]Название магазина[/]"));
            table.AddColumn(new TableColumn("[u]Id товара[/]"));
            table.AddColumn(new TableColumn("[u]Название товара[/]"));
            table.AddColumn(new TableColumn("[u]Цена товара[/]"));
            table.AddColumn(new TableColumn("[u]Количество товара[/]"));
        }
    }
}
