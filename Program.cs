// Final Version - Other Features Added
using System;

class Program
{
    static void Main()
    {
        Product[] menu = new Product[8];

        menu[0] = new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 30000, RemainingStock = 5 };
        menu[1] = new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 500, RemainingStock = 15 };
        menu[2] = new Product { Id = 3, Name = "Keyboard", Category = "Electronics", Price = 1500, RemainingStock = 10 };
        menu[3] = new Product { Id = 4, Name = "Monitor", Category = "Electronics", Price = 8000, RemainingStock = 6 };
        menu[4] = new Product { Id = 5, Name = "USB Flash Drive", Category = "Electronics", Price = 350, RemainingStock = 20 };
        menu[5] = new Product { Id = 6, Name = "Headphones", Category = "Electronics", Price = 1200, RemainingStock = 12 };
        menu[6] = new Product { Id = 7, Name = "T-Shirt", Category = "Clothing", Price = 400, RemainingStock = 18 };
        menu[7] = new Product { Id = 8, Name = "Chocolate", Category = "Food", Price = 150, RemainingStock = 25 };

        CartItem[] cart = new CartItem[20];
        int cartSize = 0;

        OrderHistory[] history = new OrderHistory[20];
        int historyCount = 0;

        bool running = true;
        int receiptCounter = 1;

        while (running)
        {
            Console.WriteLine("\n===== SHOPPING CART SYSTEM =====");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Search Product");
            Console.WriteLine("3. Filter by Category");
            Console.WriteLine("4. Add To Cart");
            Console.WriteLine("5. View Cart");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. View Order History");
            Console.WriteLine("8. Exit");

            Console.Write("Enter choice: ");
            string input = Console.ReadLine();

            int choice;

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    DisplayProducts(menu);
                    break;

                case 2:
                    SearchProduct(menu);
                    break;

                case 3:
                    FilterCategory(menu);
                    break;

                case 4:
                    AddToCart(menu, cart, ref cartSize);
                    break;

                case 5:
                    ViewCart(cart, ref cartSize);
                    break;

                case 6:
                    Checkout(cart, ref cartSize, history, ref historyCount, ref receiptCounter, menu);
                    break;

                case 7:
                    ViewOrderHistory(history, historyCount);
                    break;

                case 8:
                    running = false;
                    Console.WriteLine("Program ended.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void DisplayProducts(Product[] products)
    {
        Console.WriteLine("\n===== PRODUCT MENU =====");

        for (int i = 0; i < products.Length; i++)
        {
            products[i].DisplayProduct();
        }
    }

    static void SearchProduct(Product[] products)
    {
        Console.Write("\nEnter product name to search: ");
        string search = Console.ReadLine().ToLower();

        bool found = false;

        for (int i = 0; i < products.Length; i++)
        {
            if (products[i].Name.ToLower().Contains(search))
            {
                products[i].DisplayProduct();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Product not found.");
        }
    }

    static void FilterCategory(Product[] products)
    {
        Console.Write("\nEnter category: ");
        string category = Console.ReadLine().ToLower();

        bool found = false;

        for (int i = 0; i < products.Length; i++)
        {
            if (products[i].Category.ToLower() == category)
            {
                products[i].DisplayProduct();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No products found in this category.");
        }
    }

    static void AddToCart(Product[] products, CartItem[] cart, ref int cartSize)
    {
        DisplayProducts(products);

        Console.Write("\nEnter product ID: ");
        string prodInput = Console.ReadLine();

        int productId;

        if (!int.TryParse(prodInput, out productId))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        Product selected = null;

        for (int i = 0; i < products.Length; i++)
        {
            if (products[i].Id == productId)
            {
                selected = products[i];
                break;
            }
        }

        if (selected == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter quantity: ");
        string qtyInput = Console.ReadLine();

        int qty;

        if (!int.TryParse(qtyInput, out qty) || qty <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (!selected.HasEnoughStock(qty))
        {
            Console.WriteLine("Not enough stock.");
            return;
        }

        bool exists = false;

        for (int i = 0; i < cartSize; i++)
        {
            if (cart[i].Product.Id == selected.Id)
            {
                cart[i].Quantity += qty;
                cart[i].Subtotal += selected.GetItemTotal(qty);
                exists = true;
                break;
            }
        }

        if (!exists)
        {
            cart[cartSize] = new CartItem();
            cart[cartSize].Product = selected;
            cart[cartSize].Quantity = qty;
            cart[cartSize].Subtotal = selected.GetItemTotal(qty);

            cartSize++;
        }

        selected.DeductStock(qty);

        Console.WriteLine("Item added to cart.");
    }

    static void ViewCart(CartItem[] cart, ref int cartSize)
    {
        Console.WriteLine("\n===== CART =====");

        if (cartSize == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        double total = 0;

        for (int i = 0; i < cartSize; i++)
        {
            Console.WriteLine(
                (i + 1) + ". " +
                cart[i].Product.Name +
                " x" + cart[i].Quantity +
                " = PHP " + cart[i].Subtotal
            );

            total += cart[i].Subtotal;
        }

        Console.WriteLine("Total: PHP " + total);

        Console.WriteLine("\n1. Update Quantity");
        Console.WriteLine("2. Remove Item");
        Console.WriteLine("3. Clear Cart");
        Console.WriteLine("4. Back");

        Console.Write("Choice: ");
        string input = Console.ReadLine();

        int choice;

        if (!int.TryParse(input, out choice))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        switch (choice)
        {
            case 1:
                UpdateQuantity(cart, cartSize);
                break;

            case 2:
                RemoveItem(cart, ref cartSize);
                break;

            case 3:
                cartSize = 0;
                Console.WriteLine("Cart cleared.");
                break;

            case 4:
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void UpdateQuantity(CartItem[] cart, int cartSize)
    {
        Console.Write("Enter cart item number: ");
        int item = Convert.ToInt32(Console.ReadLine());

        if (item < 1 || item > cartSize)
        {
            Console.WriteLine("Invalid item.");
            return;
        }

        Console.Write("Enter new quantity: ");
        int qty = Convert.ToInt32(Console.ReadLine());

        if (qty <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        cart[item - 1].Quantity = qty;
        cart[item - 1].Subtotal =
            cart[item - 1].Product.Price * qty;

        Console.WriteLine("Quantity updated.");
    }

    static void RemoveItem(CartItem[] cart, ref int cartSize)
    {
        Console.Write("Enter item number to remove: ");
        int item = Convert.ToInt32(Console.ReadLine());

        if (item < 1 || item > cartSize)
        {
            Console.WriteLine("Invalid item.");
            return;
        }

        for (int i = item - 1; i < cartSize - 1; i++)
        {
            cart[i] = cart[i + 1];
        }

        cartSize--;

        Console.WriteLine("Item removed.");
    }

    static void Checkout(
        CartItem[] cart,
        ref int cartSize,
        OrderHistory[] history,
        ref int historyCount,
        ref int receiptCounter,
        Product[] products)
    {
        if (cartSize == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        double total = 0;

        Console.WriteLine("\n========== RECEIPT ==========");

        string receiptNo = "REC" + receiptCounter.ToString("000");

        Console.WriteLine("Receipt No: " + receiptNo);
        Console.WriteLine("Date: " + DateTime.Now);

        Console.WriteLine();

        for (int i = 0; i < cartSize; i++)
        {
            Console.WriteLine(
                cart[i].Product.Name +
                " x" + cart[i].Quantity +
                " = PHP " + cart[i].Subtotal
            );

            total += cart[i].Subtotal;
        }

        Console.WriteLine();

        Console.WriteLine("Grand Total: PHP " + total);

        double discount = 0;

        if (total >= 5000)
        {
            discount = total * 0.10;
            Console.WriteLine("Discount (10%): PHP " + discount);
        }

        double finalTotal = total - discount;

        Console.WriteLine("Final Total: PHP " + finalTotal);

        double payment;

        while (true)
        {
            Console.Write("Enter payment: PHP ");

            string paymentInput = Console.ReadLine();

            if (!double.TryParse(paymentInput, out payment))
            {
                Console.WriteLine("Invalid payment.");
                continue;
            }

            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment.");
                continue;
            }

            break;
        }

        double change = payment - finalTotal;

        Console.WriteLine("Change: PHP " + change);

        history[historyCount] = new OrderHistory();
        history[historyCount].ReceiptNumber = receiptNo;
        history[historyCount].Date = DateTime.Now.ToString();
        history[historyCount].FinalTotal = finalTotal;

        historyCount++;
        receiptCounter++;

        cartSize = 0;

        Console.WriteLine("\nLOW STOCK ALERT");

        for (int i = 0; i < products.Length; i++)
        {
            if (products[i].RemainingStock <= 5)
            {
                Console.WriteLine(
                    products[i].Name +
                    " only has " +
                    products[i].RemainingStock +
                    " stock(s) left."
                );
            }
        }
    }

    static void ViewOrderHistory(OrderHistory[] history, int historyCount)
    {
        Console.WriteLine("\n===== ORDER HISTORY =====");

        if (historyCount == 0)
        {
            Console.WriteLine("No orders yet.");
            return;
        }

        for (int i = 0; i < historyCount; i++)
        {
            Console.WriteLine(
                "Receipt #: " + history[i].ReceiptNumber +
                " | Date: " + history[i].Date +
                " | Total: PHP " + history[i].FinalTotal
            );
        }
    }
}
