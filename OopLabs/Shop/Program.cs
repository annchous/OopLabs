﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Shop.Core;
using Spectre.Console;

[assembly: InternalsVisibleTo("ShopTest")]

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

            Shop shop1 = new Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));
            shop1.AddProduct(pasta, new ProductStatus(54, 392));
            shop1.AddProduct(beer, new ProductStatus(89, 666));
            shop1.AddProduct(toiletPaper, new ProductStatus(120, 426));

            Shop shop2 = new Shop("Перекрёсток", "Денежная ул. д.98");
            shop2.AddProduct(apple, new ProductStatus(30, 832));
            shop2.AddProduct(banana, new ProductStatus(41, 892));
            shop2.AddProduct(milk, new ProductStatus(65, 278));
            shop2.AddProduct(pasta, new ProductStatus(69, 2953));
            shop2.AddProduct(beer, new ProductStatus(47, 382));
            shop2.AddProduct(redBull, new ProductStatus(127, 284));
            shop2.AddProduct(bread, new ProductStatus(33, 1793));

            Shop shop3 = new Shop("Дикси", "Лесной пр-кт д.9");
            shop3.AddProduct(apple, new ProductStatus(19, 1938));
            shop3.AddProduct(banana, new ProductStatus(41, 892));
            shop3.AddProduct(milk, new ProductStatus(65, 278));
            shop3.AddProduct(pasta, new ProductStatus(69, 2953));
            shop3.AddProduct(shampoo, new ProductStatus(155, 329));
            shop3.AddProduct(tomato, new ProductStatus(25, 394));

            var shopList = new List<Shop> {shop1, shop2, shop3};
            ShopList shops = new ShopList(shopList);

            var list = shop1.GetProductsOnSum(100);

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            AnsiConsole.WriteLine("Список всех магазинов с товарами");
            shops.PrintShops();

            AnsiConsole.WriteLine("Список товаров, которые можно купить на 100 рублей в магазине " + shop1.Name);
            ShopPrinter.PrintProductList(list, shop1);

            var sum = shop1.BuyLotOfProducts(new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(45)),
                new ProductRequest(banana, new ProductStatus(103)),
                new ProductRequest(milk, new ProductStatus(15)),
            }));

            AnsiConsole.WriteLine("Сумма покупки составила: " + sum);

            AnsiConsole.WriteLine("Список всех магазинов с товарами");
            shops.PrintShops();

            var x = shops.GetShopWithLowestPriceOn("P1");
            AnsiConsole.WriteLine("Магазин с наименьшей ценой на продукт с id P1: " + x.Name);

            var lot = new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(10)),
                new ProductRequest(banana, new ProductStatus(10)),
            });

            var shop = shops.GetShopWithLowestSumOnLot(lot);
            AnsiConsole.WriteLine("Магазин с наименьшей стоимостью покупки набора товаров: " + shop.Name);

            var copiedShop1 = shop1.CopyWith(shop1.Id, shop1.Name, shop1.Address, shop1.Products);
            copiedShop1.PrintShop();
        }
    }
}
