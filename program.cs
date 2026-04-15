// Version 3 - Final checkout and discount logic
using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine(Id + ". " + Name + " - PHP " + Price + " | Stock: " + RemainingStock);
    }

    public double GetItemTotal(int qty)
    {
        return Price * qty;
    }

    public bool HasEnoughStock(int qty)
    {
        return qty <= RemainingStock;
    }

    public void DeductStock(int qty)
    {
        RemainingStock -= qty;
    }
}

class CartItem
{
    public Product product;
    public int quantity;
    public double subtotal;
}

class Program
{
    static void Main()
    {
        // ✅ UPDATED PRODUCT LIST
        Product[] menu = new Product[8];

        menu[0] = new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 };
        menu[1] = new Product { Id = 2, Name = "Mouse", Price = 500, RemainingStock = 15 };
        menu[2] = new Product { Id = 3, Name = "Keyboard", Price = 1500, RemainingStock = 10 };
        menu[3] = new Product { Id = 4, Name = "Monitor", Price = 8000, RemainingStock = 6 };
        menu[4] = new Product { Id = 5, Name = "USB Flash Drive", Price = 350, RemainingStock = 20 };
        menu[5] = new Product { Id = 6, Name = "Headphones", Price = 1200, RemainingStock = 12 };
        menu[6] = new Product { Id = 7, Name = "Webcam", Price = 2200, RemainingStock = 7 };
        menu[7] = new Product { Id = 8, Name = "External Hard Drive", Price = 4500, RemainingStock = 5 };

        CartItem[] cart = new CartItem[5];
        int cartSize = 0;

        string answer = "Y";

        while (answer == "Y")
        {
            Console.WriteLine("\n==== STORE MENU ====");
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].DisplayProduct();
            }

            Console.Write("Enter product number: ");
            string inputProd = Console.ReadLine();

            int prodNum;
            if (!int.TryParse(inputProd, out prodNum) || prodNum < 1 || prodNum > menu.Length)
            {
                Console.WriteLine("Invalid product number.");
                continue;
            }

            Product selected = menu[prodNum - 1];

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("This product is out of stock.");
                continue;
            }

            Console.Write("Enter quantity: ");
            string inputQty = Console.ReadLine();

            int qty;
            if (!int.TryParse(inputQty, out qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                continue;
            }

            if (!selected.HasEnoughStock(qty))
            {
                Console.WriteLine("Not enough stock available.");
                continue;
            }

            bool exists = false;

            for (int i = 0; i < cartSize; i++)
            {
                if (cart[i].product.Id == selected.Id)
                {
                    cart[i].quantity += qty;
                    cart[i].subtotal += selected.GetItemTotal(qty);
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                if (cartSize >= cart.Length)
                {
                    Console.WriteLine("Cart is full.");
                    continue;
                }

                cart[cartSize] = new CartItem();
                cart[cartSize].product = selected;
                cart[cartSize].quantity = qty;
                cart[cartSize].subtotal = selected.GetItemTotal(qty);

                cartSize++;
            }

            selected.DeductStock(qty);

            Console.WriteLine("Added to cart!");

            Console.Write("Do you want to add more? (Y/N): ");
            answer = Console.ReadLine().ToUpper();
        }

        // Receipt
        Console.WriteLine("\n==== RECEIPT ====");
        double total = 0;

        for (int i = 0; i < cartSize; i++)
        {
            Console.WriteLine(cart[i].product.Name + " x" + cart[i].quantity + " = PHP " + cart[i].subtotal);
            total += cart[i].subtotal;
        }

        Console.WriteLine("Grand Total: PHP " + total);

        double discount = 0;

        if (total >= 5000)
        {
            discount = total * 0.10;
            Console.WriteLine("Discount (10%): PHP " + discount);
        }

        double finalTotal = total - discount;

        Console.WriteLine("Final Total: PHP " + finalTotal);

        Console.WriteLine("\n==== UPDATED STOCK ====");
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].DisplayProduct();
        }

        Console.WriteLine("\nProgram finished.");
    }
}
