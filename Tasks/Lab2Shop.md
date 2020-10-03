## Shop
### Laboratory work №2

### [Solution](https://github.com/annchous/OopLabs/tree/master/OopLabs/Shop)
### [Tests]()

There are **Products** that are sold in **Stores**.
* Stores have a code (unique), a name (not necessarily unique) and an address.
* Products have a code (unique), a name (not necessarily unique).

Each store has its own price for the product and there is a certain number of units in stock (some product may not be available at all).

### Make methods for the following operations:

1. Create a store.
2. Create a product.
3. Bring a batch of goods to the store (set of goods-quantity with the ability to set/change the price).
4. Find a store in which a certain product is the cheapest.
5. Understand what goods can be bought in a store for a certain amount (for example, for 100 rubles you can buy three kg of cucumbers or two chocolates).
6. Buy a batch of goods in the store (parameters - how many of which goods to buy, the method returns the total purchase price or its impossibility if there is not enough goods).
7. Find in which store the batch of goods (set item-quantity) has the smallest amount (in total). For example, "which store is the cheapest to buy 10 nails and 20 screws." Availability of goods in stores is taken into account!

For demonstration you need to create at least 3 different stores, 10 types of goods and fill the stores with them.

*The above methods should be displayed in tests.*

### Code description

#### Product.cs

Represents the entity of a product. Contains an ```Id``` field with an auto-assigned value and a ```Name``` field.

#### ProductStatus.cs

Represents the entity of the characteristics of a product displayed in a store. Contains the ```Price``` and ```Amount``` fields.

#### ProductRequest.cs

Represents the single entity of the product displayed in the store. Contains the fields ```Product``` and ```ProductStatus```.

#### ProductLot.cs

Represents the entity of a consignment. Contains a ```List<ProductRequest> Lot``` field.

Has 6 overloads of the ```AddToLot``` method:
```
public void AddToLot(ProductRequest productRequest)
public void AddToLot(Product product, ProductStatus productStatus)
public void AddToLot(Product product)
public void AddToLot(Product product, decimal price, int amount)
public void AddToLot(Product product, decimal price)
public void AddToLot(Product product, int amount)
```

Has 2 overloads of the ```SetPrice``` method:
```
public void SetPrice(string id, decimal price)
public void SetPrice(Product product, decimal price)
```

#### Shop.cs

Represents the entity of a shop. Contains an ```Id``` field with an auto-assigned value and ```Name```, ```Address ``` and ```List<ProductRequest> Products``` fields.

Has 5 overloads of the ```AddProduct``` method:
```
public void AddProduct(Product product)
public void AddProduct(Product product, ProductStatus productStatus)
public void AddProduct(Product product, decimal price)
public void AddProduct(Product product, int amount)
public void AddProduct(Product product, decimal price, int amount) 
```

Has 2 overloads of the ```AddProductLot``` method:
```
public void AddProductLot(ProductLot lot)
public void AddProductLot(Product product, ProductStatus productStatus)
```

Contains a method ```public List<ProductRequest> GetProductsOnSum(decimal price)``` that implements item 5 of the task condition.

Contains a method ```public decimal BuyLotOfProducts(ProductLot lot)``` that implements item 6 of the task condition (throws an exception if a purchase is not possible).

#### ShopList.cs

Represents an entity that stores a list of shops. Contains an ```public List<Shop> Shops``` field.

Contains a method ```public Shop GetShopWithLowestPriceOn(string id)``` that implements item 4 of the task condition.

Contains a method ```public Shop GetShopWithLowestSumOnLot(ProductLot lot)``` that implements item 7 of the task condition.

#### ShopException.cs

Represents a custom exception class.

#### ShopPrinter.cs

Represents a class for formatted console output as a table.

![Shop](https://github.com/annchous/OopLabs/blob/lab2/Tasks/shop.PNG)

#### Program.cs

A class with a ```Main``` method.
