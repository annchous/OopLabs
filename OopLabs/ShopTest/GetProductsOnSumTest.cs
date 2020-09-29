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
    public class GetProductsOnSumTest
    {
        [TestMethod]
        public void ProductsUnderSumNotExist()
        {
            Product apple = new Product("Яблоко");
            Product banana = new Product("Банан");
            Product milk = new Product("Молоко");
            Product toiletPaper = new Product("Туалетная бумага");
            Product beer = new Product("Пиво");
            Product pasta = new Product("Макароны");

            Shop.Shop shop1 = new Shop.Shop("Пятёрочка", "Хвостовая ул. д.239");
            shop1.AddProduct(apple, new ProductStatus(25, 1836));
            shop1.AddProduct(banana, new ProductStatus(34, 993));
            shop1.AddProduct(milk, new ProductStatus(67, 378));
            shop1.AddProduct(pasta, new ProductStatus(54, 392));
            shop1.AddProduct(beer, new ProductStatus(89, 666));
            shop1.AddProduct(toiletPaper, new ProductStatus(120, 426));

            Assert.ThrowsException<ImpossibleToBuyException>(() => shop1.GetProductsOnSum(1));
        }
    }
}
