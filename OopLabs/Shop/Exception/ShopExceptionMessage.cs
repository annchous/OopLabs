using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Exception
{
    static class ShopExceptionMessage
    {
        public static string ProductNotFound = "Product was not found!";
        public static string ShopNotFound = "Shop was not found!";
        public static string ImpossibleToBuy = "Impossible to buy any product with ";
        public static string ImpossibleToBuyLot = "Impossible to buy this lot!";
    }
}