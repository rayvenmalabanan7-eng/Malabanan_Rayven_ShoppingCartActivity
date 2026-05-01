using System;

class Product
{
    public int Id;
    public string Name;
    public string Category;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine(
            Id + ". " + Name +
            " | Category: " + Category +
            " | PHP " + Price +
            " | Stock: " + RemainingStock
        );
    }

    public bool HasEnoughStock(int qty)
    {
        return qty <= RemainingStock;
    }

    public void DeductStock(int qty)
    {
        RemainingStock -= qty;
    }

    public double GetItemTotal(int qty)
    {
        return Price * qty;
    }
}