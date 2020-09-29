using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.Core;
using Shop.Exception.ShopException;

namespace ShopTest
{
    [TestClass]
    public class BuyLotOfProductsTest
    {
        [TestMethod]
        public void PurchaseNotPossibleTest()
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");
            Product milk = new Product("Молоко");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));

            var lot = new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(45000)),
                new ProductRequest(banana, new ProductStatus(103)),
                new ProductRequest(milk, new ProductStatus(15)),
            });

            var result = Assert.ThrowsException<ImpossibleToBuyLotException>(() => shop1.BuyLotOfProducts(lot));
            Assert.AreEqual("Impossible to buy this lot!", result.Message);
        }

        [TestMethod]
        public void PurchasePossibleCheckSumTest()
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");
            Product milk = new Product("Молоко");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));

            var lot = new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(45)),
                new ProductRequest(banana, new ProductStatus(103)),
                new ProductRequest(milk, new ProductStatus(15)),
            });

            var sum = shop1.BuyLotOfProducts(lot);
            Assert.AreEqual(5632.0m, sum);
        }

        [TestMethod]
        public void PurchasePossibleCheckAmountTest()
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");
            Product milk = new Product("Молоко");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));

            var lot = new ProductLot(new List<ProductRequest>
            {
                new ProductRequest(apple, new ProductStatus(45)),
                new ProductRequest(banana, new ProductStatus(103)),
                new ProductRequest(milk, new ProductStatus(15)),
            });

            shop1.BuyLotOfProducts(lot);
            var appleAmount = shop1.Products
                .Where(x => x.Product.Id == apple.Id)
                .Select(x => x.ProductStatus.Amount)
                .FirstOrDefault();
            Assert.AreEqual(1791, appleAmount);
        }
    }
}