﻿using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core;
using Spectre.Console;

namespace Shop
{
    static class ShopPrinter
    {
        public static void CreateTableHeader(Table table)
        {
            table.AddColumn(new TableColumn("[underline white]Id магазина[/]"));
            table.AddColumn(new TableColumn("[underline white]Название магазина[/]"));
            table.AddColumn(new TableColumn("[underline white]Адрес магазина[/]"));
            table.AddColumn(new TableColumn("[underline white]Id товара[/]"));
            table.AddColumn(new TableColumn("[underline white]Название товара[/]"));
            table.AddColumn(new TableColumn("[underline white]Цена товара[/]"));
            table.AddColumn(new TableColumn("[underline white]Количество товара[/]"));
        }

        public static void PrintShop(Shop shop, Table table)
        {
            foreach (var item in shop.Products)
            {
                table.AddRow($"[red]{shop.Id}[/]", $"[white]{shop.Name}[/]",
                    $"[fuchsia]{shop.Address}[/]", $"[aqua]{item.Product.Id}[/]",
                    $"[white]{item.Product.Name}[/]", $"[green]{item.ProductStatus.Price}[/]",
                    $"[yellow]{item.ProductStatus.Amount}[/]");
            }
        }

        public static void PrintShopList(ShopList shops)
        {
            var table = new Table();
            CreateTableHeader(table);
            foreach (var shop in shops.Shops)
            {
                PrintShop(shop, table);
            }
            AnsiConsole.Render(table);
        }

        public static void PrintSingleShop(Shop shop)
        {
            var table = new Table();
            CreateTableHeader(table);
            PrintShop(shop, table);
            AnsiConsole.Render(table);
        }

        public static void PrintProductList(List<ProductRequest> productList, Shop shop)
        {
            var table = new Table();
            CreateTableHeader(table);
            foreach (var product in productList)
            {
                AddProductToTable(product, shop, table);
            }
            AnsiConsole.Render(table);
        }

        public static void AddProductToTable(ProductRequest product, Shop shop, Table table) => 
            table.AddRow(
            $"[red]{shop.Id}[/]", $"[white]{shop.Name}[/]",
            $"[fuchsia]{shop.Address}[/]", $"[aqua]{product.Product.Id}[/]",
            $"[white]{product.Product.Name}[/]", $"[green]{product.ProductStatus.Price}[/]",
            $"[yellow]{product.ProductStatus.Amount}[/]");
    }
}
