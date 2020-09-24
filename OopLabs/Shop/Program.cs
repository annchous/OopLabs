using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");
            Product bread = new Product("Хлеб");
            Product milk = new Product("Молоко");
            Product tomato = new Product("Помидор");
            Product shampoo = new Product("Шампунь");
            Product toiletPaper = new Product("Туалетная бумага");
            Product beer = new Product("Пиво");
            Product pasta = new Product("Макароны");
            Product redBull = new Product("РедБулл");

            Shop shop1 = new Shop("Пятёрочка");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));
            shop1.AddProduct(pasta, new ProductStatus(54, 392));
            shop1.AddProduct(beer, new ProductStatus(89, 666));
            shop1.AddProduct(toiletPaper, new ProductStatus(120, 426));

            Shop shop2 = new Shop("Перекрёсток");
            shop2.AddProduct(apple, new ProductStatus(30, 832));
            shop2.AddProduct(banana, new ProductStatus(41, 892));
            shop2.AddProduct(milk, new ProductStatus(65, 278));
            shop2.AddProduct(pasta, new ProductStatus(69, 2953));
            shop2.AddProduct(beer, new ProductStatus(47, 382));
            shop2.AddProduct(redBull, new ProductStatus(127, 284));
            shop2.AddProduct(bread, new ProductStatus(33, 1793));

            Shop shop3 = new Shop("Дикси");
            shop3.AddProduct(apple, new ProductStatus(19, 1938));
            shop3.AddProduct(banana, new ProductStatus(41, 892));
            shop3.AddProduct(milk, new ProductStatus(65, 278));
            shop3.AddProduct(pasta, new ProductStatus(69, 2953));
            shop3.AddProduct(shampoo, new ProductStatus(155, 329));
            shop3.AddProduct(tomato, new ProductStatus(25, 394));

            var shopList = new List<Shop> {shop1, shop2, shop3};
            ShopList shops = new ShopList(shopList);

            Dictionary<Shop, Dictionary<Product, ProductStatus>> list = shops.GetProductsOnSum(100);

            AnsiConsole.WriteLine("Бюджет: 100 рублей");
            var table = new Table();
            table.AddColumn(new TableColumn("[u]Id магазина[/]"));
            table.AddColumn(new TableColumn("[u]Название магазина[/]"));
            table.AddColumn(new TableColumn("[u]Id товара[/]"));
            table.AddColumn(new TableColumn("[u]Название товара[/]"));
            table.AddColumn(new TableColumn("[u]Цена товара[/]"));
            table.AddColumn(new TableColumn("[u]Количество товара[/]"));

            foreach (var shop in list)
            {
                foreach (var product in shop.Value)
                {
                    table.AddRow($"[red]{shop.Key.Id}[/]", $"[white]{shop.Key.Name}[/]", $"[aqua]{product.Key.Id}[/]",
                        $"[white]{product.Key.Name}[/]", $"[green]{product.Value.Price}[/]",
                        $"[yellow]{product.Value.Amount}[/]");
                }
            }

            AnsiConsole.Render(table);

            shops.PrintShops();
        }
    }
}
