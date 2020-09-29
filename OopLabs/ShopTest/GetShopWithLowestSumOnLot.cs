using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.Core;
using Shop.Exception.ShopException;

namespace ShopTest
{
    [TestClass]
    public class GetShopWithLowestSumOnLot
    {
        [TestMethod]
        public void AllProductsAvailableTest()
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));

            Shop.Shop shop2 = new Shop.Shop("Перекрёсток", "Денежная ул. д.98");
            shop2.AddProduct(apple, new ProductStatus(30, 832));
            shop2.AddProduct(banana, new ProductStatus(41, 892));

            Shop.Shop shop3 = new Shop.Shop("Дикси", "Лесной пр-кт д.9");
            shop3.AddProduct(apple, new ProductStatus(19, 1938));
            shop3.AddProduct(banana, new ProductStatus(41, 892));

            var shopList = new List<Shop.Shop> { shop1, shop2, shop3 };
            ShopList shops = new ShopList(shopList);

            var lot = new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(10)),
                new ProductRequest(banana, new ProductStatus(10)),
            });

            var shop = shops.GetShopWithLowestSumOnLot(lot);
            Assert.AreEqual("Пятёрочка", shop.Name);
        }

        [TestMethod]
        public void SomeProductsNotAvailableTest()
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");
            Product milk = new Product("Молоко");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));

            Shop.Shop shop2 = new Shop.Shop("Перекрёсток", "Денежная ул. д.98");
            shop2.AddProduct(apple, new ProductStatus(30, 832));
            shop2.AddProduct(banana, new ProductStatus(41, 892));

            Shop.Shop shop3 = new Shop.Shop("Дикси", "Лесной пр-кт д.9");
            shop3.AddProduct(apple, new ProductStatus(19, 1938));
            shop3.AddProduct(banana, new ProductStatus(41, 892));

            var shopList = new List<Shop.Shop> { shop1, shop2, shop3 };
            ShopList shops = new ShopList(shopList);

            var lot = new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(10)),
                new ProductRequest(banana, new ProductStatus(10)),
                new ProductRequest(milk, new ProductStatus(10))
            });

            var result = Assert.ThrowsException<ShopNotFoundException>(() => shops.GetShopWithLowestSumOnLot(lot));
            Assert.AreEqual("Shop was not found!", result.Message);
        }

        [TestMethod]
        public void SomeProductsAvailableOnlyInOneShopTest()
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");
            Product milk = new Product("Молоко");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));

            Shop.Shop shop2 = new Shop.Shop("Перекрёсток", "Денежная ул. д.98");
            shop2.AddProduct(apple, new ProductStatus(30, 832));
            shop2.AddProduct(banana, new ProductStatus(41, 892));
            shop2.AddProduct(milk, new ProductStatus(478));

            Shop.Shop shop3 = new Shop.Shop("Дикси", "Лесной пр-кт д.9");
            shop3.AddProduct(apple, new ProductStatus(19, 1938));
            shop3.AddProduct(banana, new ProductStatus(41, 892));

            var shopList = new List<Shop.Shop> { shop1, shop2, shop3 };
            ShopList shops = new ShopList(shopList);

            var lot = new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(10)),
                new ProductRequest(banana, new ProductStatus(10)),
                new ProductRequest(milk, new ProductStatus(10))
            });

            var shop = shops.GetShopWithLowestSumOnLot(lot);
            Assert.AreEqual("Перекрёсток", shop.Name);
        }
    }
}
