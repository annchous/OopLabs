using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.Core;
using Shop.Exception.ShopException;
using System;
using System.Text;

namespace ShopTest
{
    [TestClass]
    public class GetShopWithLowestPriceOnTest
    {
        [TestMethod]
        public void AllProductsAvailableTest()
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

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));
            shop1.AddProduct(pasta, new ProductStatus(54, 392));
            shop1.AddProduct(beer, new ProductStatus(89, 666));
            shop1.AddProduct(toiletPaper, new ProductStatus(120, 426));

            Shop.Shop shop2 = new Shop.Shop("Перекрёсток", "Денежная ул. д.98");
            shop2.AddProduct(apple, new ProductStatus(30, 832));
            shop2.AddProduct(banana, new ProductStatus(41, 892));
            shop2.AddProduct(milk, new ProductStatus(65, 278));
            shop2.AddProduct(pasta, new ProductStatus(69, 2953));
            shop2.AddProduct(beer, new ProductStatus(47, 382));
            shop2.AddProduct(redBull, new ProductStatus(127, 284));
            shop2.AddProduct(bread, new ProductStatus(33, 1793));

            Shop.Shop shop3 = new Shop.Shop("Дикси", "Лесной пр-кт д.9");
            shop3.AddProduct(apple, new ProductStatus(19, 1938));
            shop3.AddProduct(banana, new ProductStatus(41, 892));
            shop3.AddProduct(milk, new ProductStatus(65, 278));
            shop3.AddProduct(pasta, new ProductStatus(69, 2953));
            shop3.AddProduct(shampoo, new ProductStatus(155, 329));
            shop3.AddProduct(tomato, new ProductStatus(25, 394));

            var shopList = new List<Shop.Shop> { shop1, shop2, shop3 };
            ShopList shops = new ShopList(shopList);

            var result1 = shops.GetShopWithLowestPriceOn(apple);
            var result2 = shops.GetShopWithLowestPriceOn(milk);
            var result3 = shops.GetShopWithLowestPriceOn(tomato);

            Assert.AreEqual("Дикси", result1.Name);
            Assert.AreEqual("Перекрёсток", result2.Name);
            Assert.AreEqual("Дикси", result3.Name);
        }

        [TestMethod]
        public void ProductNotAvailableTest()
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

            Product cucumber = new Product("Огурец");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));
            shop1.AddProduct(pasta, new ProductStatus(54, 392));
            shop1.AddProduct(beer, new ProductStatus(89, 666));
            shop1.AddProduct(toiletPaper, new ProductStatus(120, 426));

            Shop.Shop shop2 = new Shop.Shop("Перекрёсток", "Денежная ул. д.98");
            shop2.AddProduct(apple, new ProductStatus(30, 832));
            shop2.AddProduct(banana, new ProductStatus(41, 892));
            shop2.AddProduct(milk, new ProductStatus(65, 278));
            shop2.AddProduct(pasta, new ProductStatus(69, 2953));
            shop2.AddProduct(beer, new ProductStatus(47, 382));
            shop2.AddProduct(redBull, new ProductStatus(127, 284));
            shop2.AddProduct(bread, new ProductStatus(33, 1793));

            Shop.Shop shop3 = new Shop.Shop("Дикси", "Лесной пр-кт д.9");
            shop3.AddProduct(apple, new ProductStatus(19, 1938));
            shop3.AddProduct(banana, new ProductStatus(41, 892));
            shop3.AddProduct(milk, new ProductStatus(65, 278));
            shop3.AddProduct(pasta, new ProductStatus(69, 2953));
            shop3.AddProduct(shampoo, new ProductStatus(155, 329));
            shop3.AddProduct(tomato, new ProductStatus(25, 394));

            var shopList = new List<Shop.Shop> { shop1, shop2, shop3 };
            ShopList shops = new ShopList(shopList);

            var result1 = Assert.ThrowsException<ShopNotFoundException>(() => shops.GetShopWithLowestPriceOn("P120"));
            var result2 = Assert.ThrowsException<ShopNotFoundException>(() => shops.GetShopWithLowestPriceOn(cucumber));

            Assert.AreEqual("Shop was not found!", result1.Message);
            Assert.AreEqual("Shop was not found!", result2.Message);
        }
    }
}
