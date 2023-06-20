using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
try
{
    VendingMachine vendingMachine1 = new VendingMachine("123");
    Console.WriteLine(vendingMachine1.Barcode);
    Console.WriteLine(vendingMachine1.SerialNumber);

    VendingMachine vendingMachine2 = new VendingMachine("1234");
    Console.WriteLine(vendingMachine2.Barcode);
    Console.WriteLine(vendingMachine2.SerialNumber);

    Product apple = new Product("Apple", 2, "A1");

    Console.WriteLine(vendingMachine1.StockItem(apple, 10));
    Console.WriteLine(vendingMachine1.StockFloat(1, 5));
    Console.WriteLine(vendingMachine1.VendItem("A1", new List<int> { 5 }));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

public class VendingMachine
{
    private static int _nextSerialNumber = 1;

    private Dictionary<int, int> _moneyFloat = new Dictionary<int, int>();
    private Dictionary<Product, int> _inventory = new Dictionary<Product, int>();

    public int SerialNumber { get; }
    public string Barcode { get; }

    public VendingMachine(string barcode)
    {
        try
        {
            if (string.IsNullOrEmpty(barcode))
            {
                throw new ArgumentException("Not allow its `Barcode` to be empty.");
            }

            Barcode = barcode;
            SerialNumber = _nextSerialNumber++;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public string StockItem(Product product, int quantity)
    {
        try
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Not allow negative quantities.");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentException("Should not create a new object if the `Name` strings are null/empty.");
            }

            if (string.IsNullOrEmpty(product.Code))
            {
                throw new ArgumentException("Should not create a new object if the `Code` strings are null/empty.");
            }

            if (product.Price <= 0)
            {
                throw new ArgumentException("should not create a new object if the Price` is negative/zero.");
            }

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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
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
        try
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Code should not be null/empty.");
            }

            if (money == null || money.Count == 0)
            {
                throw new ArgumentException("Money list should not be null/empty.");
            }

            if (money.Any(m => m <= 0))
            {
                throw new ArgumentException("Money should not be less than or equal to zero.");
            }

            int sum = money.Sum();

            foreach (var product in _inventory.Keys)
            {
                if (product.Code == code)
                {
                    if (_inventory[product] > 0)
                    {
                        if (product.Price <= sum)
                        {
                            int difference = sum - product.Price;
                            _inventory[product]--;

                            Dictionary<int, int> change = CalculateChange(difference);
                            UpdateMoneyFloat(change);

                            return $"Please enjoy your '{product.Name}' and take your change of ${difference}.";
                        }
                        else
                        {
                            return "Error: Insufficient money provided.";
                        }
                    }
                    else
                    {
                        return "Error: Item is out of stock.";
                    }
                }
            }

            return $"Error: No item with code '{code}' found.";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    private Dictionary<int, int> CalculateChange(int difference)
    {
        Dictionary<int, int> change = new Dictionary<int, int>();

        List<int> denominations = new List<int> { 20, 10, 5, 2, 1 };

        foreach (int denomination in denominations)
        {
            if (_moneyFloat.ContainsKey(denomination) && _moneyFloat[denomination] > 0)
            {
                int count = Math.Min(difference / denomination, _moneyFloat[denomination]);
                if (count > 0)
                {
                    change[denomination] = count;
                    difference -= count * denomination;
                }
            }
        }

        return change;
    }

    private void UpdateMoneyFloat(Dictionary<int, int> change)
    {
        foreach (var kvp in change)
        {
            int denomination = kvp.Key;
            int count = kvp.Value;
            _moneyFloat[denomination] -= count;
        }
    }
}

public class Product
{
    public string Name { get; }
    public int Price { get; }
    public string Code { get; }

    public Product(string name, int price, string code)
    {
        Name = name;
        Price = price;
        Code = code;
    }
}