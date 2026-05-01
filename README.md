# Shopping Cart System

## Description
This project is a console-based Shopping Cart System developed in C#.  
It allows users to select products, enter quantities, and manage a shopping cart with stock validation.

This Project is developed in 2 parts:
Part 1 - Shopping Cart Core System
Part 2 - Enhancements and Code Improvements

## Features
- Display product menu
- Input validation (product number and quantity)
- Prevents invalid and non-numeric input
- Stock checking and out-of-stock handling
- Add items to cart
- Updates existing cart items (no duplicates)
- Computes total price
- Applies 10% discount if total is 5000 or more
- Displays receipt and final total
- Shows updated remaining stock after checkout

## Part 2 Enhancements
- Improved code structure using object-oriented programming (classes and objects)
- Cleaner and more organized Program.cs
- Refactored cart logic for better readability
- Improved separation of responsibilities between classes (Product, CartItem, etc.)
- Fixed and organized project structure after initial setup issues
- Improved maintainability for future enhancements

## Files Included
- Program.cs (main program)
- Flowchart.png (program flowchart)
- CartItem.cs
- OrderHistory.cs
- Product.cs

## How to Run
1. Open the project in Visual Studio or any C# IDE
2. Run the program
3. Follow the on-screen instructions

## Author
Rayven Malabanan

## AI Usage in This Project
AI was used to assist in:
- Structuring the program using classes and objects
- Implementing input validation using int.TryParse()
- Designing the cart system and logic

Prompts used:
- "Create a C# shopping cart system using classes"
- "How to validate user input in C#"
- "How to prevent duplicate items in a cart"

Changes made after using AI:
- Simplified variable names
- Adjusted logic to match project requirements
- Improved output formatting
