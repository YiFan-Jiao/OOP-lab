using System.Collections;
using System.Collections.Generic;
using System.Linq;

VendingMachine firstMachine = new VendingMachine(123);

Product apple = new Product("apple", 2, "a1");

Console.WriteLine(firstMachine.StockItem(apple, 10));

Console.WriteLine(firstMachine.StockFloat(1, 5));

Console.WriteLine(firstMachine.VendItem("a1", new List<int> { 1, 2, 5 }));

public class VendingMachine
{
    private int _seriaNumber;

    private Dictionary<int, int> _moneyFloat = new Dictionary<int, int>();

    private Dictionary<Product, int> _inventory = new Dictionary<Product, int>();

    public int SeriaNumber { get { return _seriaNumber; } }

    public VendingMachine(int serialNumber)
    {
        _seriaNumber = serialNumber;
    }

    public string StockItem(Product product, int quantity)
    {
        if (_inventory.ContainsKey(product))
        {
            _inventory[product] += quantity;
        }
        else
        {
            _inventory[product] = quantity;
        }

        return $"Name: {product.Name}, Code: {product.Code}, Price: ${product.Price}, Quantity: {_inventory[product]}.";
    }

    public string StockFloat(int moneyDenomination, int quantity)
    {
        if (_moneyFloat.ContainsKey(moneyDenomination))
        {
            _moneyFloat[moneyDenomination] += quantity;
        }
        else
        {
            _moneyFloat[moneyDenomination] = quantity;
        }

        return $"MoneyDenomination: {moneyDenomination} dollar,quantity: {quantity}";
    }

    public string VendItem(string code, List<int> money)
    {
        List<int> list = new List<int>() { 1, 2, 5, 10, 20 };

        int sum = 0;
        foreach (int num in money)
        {
            sum += num;
        }

        foreach (var product in _inventory.Keys)
        {
            if (product.Code == code)
            {
                if (_inventory[product] > 0)
                {
                    if (product.Price <= sum)
                    {
                        int difference = sum - product.Price;
                        _inventory[product] = _inventory[product] - 1;

                        foreach (KeyValuePair<int, int> kvp in _moneyFloat)
                        {
                            //1,2,5,10,20
                            foreach (int num in money)
                            {
                                if (num == kvp.Key)
                                {
                                    //lab2
                                    _moneyFloat[num]++;
                                }
                            }
                            Console.WriteLine(kvp.Key);

                            if (difference / _moneyFloat[kvp.Key]! < 1)
                            {

                            }
                        }

                        return $"Please enjoy your ‘{code}’ and take your change of ${sum}.";
                    }
                    else
                    {
                        return $"Error: insufficient money provided";
                    }
                }
                else
                {
                    return $"Error: Item is out of stock";
                }
            }
        }

        return $"Error, no item with code “{code}.";
    }
}

public class Product
{
    public string Name { get; set; }

    public int Price { get; set; }

    public string Code { get; set; }

    public Product(string name, int price, string code)
    {
        Name = name;
        Price = price;
        Code = code;
    }
}