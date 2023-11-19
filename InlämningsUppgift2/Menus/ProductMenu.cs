using InlämningsUppgift2.Entities;
using InlämningsUppgift2.Models;
using InlämningsUppgift2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Menus
{
    internal class ProductMenu
    {
        private readonly ProductService _productService;

        public ProductMenu(ProductService productService)
        {
            _productService = productService;
        }

        public async Task ManageProducts()
        {
            Console.Clear();
            Console.WriteLine("Manage Products");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Update Products");
            Console.WriteLine("4. Delete Products");
            Console.WriteLine("5: Go back");
            Console.Write("Choose one option: ");


            var option = Console.ReadLine();


            switch (option)
            {
                case "1":
                    await CreateAsync();
                    break;

                case "2":
                    await ListAllAsync();
                    break;
                case "3":
                    await UpdateAsync();
                    break;
                case "4":
                    await DeleteAsync();
                    break;
                case "5":
                    return; 
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;

            }

        }

        public async Task CreateAsync()
        {
            var form = new ProductRegistrationForm();

            Console.Clear();
            Console.Write("Product Name: ");
            form.ProductName = Console.ReadLine()!;

            Console.Write("Product Description: ");
            form.ProductDescription = Console.ReadLine()!;

            Console.Write("Product Category: ");
            form.ProductCategory = Console.ReadLine()!;


            Console.Write("Product Price(SEK): ");
            form.Price = decimal.Parse(Console.ReadLine()!);
           
            Console.Write("Payment (Cash/Card/Other): ");
            form.MoneyPayment = Console.ReadLine()!;



           var result = await _productService.CreateProductAsync(form);
            if (result)
                Console.WriteLine("Product was created\n----------------------\nPress any key to return");
            else
                Console.WriteLine("Product could not be created");


            Console.ReadKey();



        }

        public async Task ListAllAsync()
        {
            var products = await _productService.GetAllAsync();
            foreach (var product in products)
            {
                Console.WriteLine($"Product Id: {product.Id}\nProduct Name: {product.ProductName}\nCategory: {product.ProductCategory.CategoryName}");
                Console.WriteLine($"Price: {product.ProductPrice}\nPayment: {product.MoneyPayment.Payment}");

                Console.WriteLine("");

            }
            Console.ReadKey();
        }

        public async Task UpdateAsync()
        {
            {
                Console.Clear();
                Console.Write("Enter the ID of the product to update: ");

                if (int.TryParse(Console.ReadLine(), out var productId))
                {
                    var existingProduct = await _productService.GetProductByIdAsync(productId);

                    if (existingProduct != null)
                    {
                        var form = new ProductRegistrationForm();

                        Console.Write("Product Name (Leave blank to keep the current value '{0}'): ", existingProduct.ProductName);
                        form.ProductName = Console.ReadLine() ?? existingProduct.ProductName;

                        Console.Write("Product Description (Leave blank to keep the current value '{0}'): ", existingProduct.ProductDescription);
                        form.ProductDescription = Console.ReadLine() ?? existingProduct.ProductDescription;

                        Console.Write("Product Category (Leave blank to keep the current value '{0}'): ", existingProduct.ProductCategory.CategoryName);
                        form.ProductCategory = Console.ReadLine() ?? existingProduct.ProductCategory.CategoryName;

                        Console.Write("Product Price (Leave blank to keep the current value '{0}'): ", existingProduct.ProductPrice);
                        if (decimal.TryParse(Console.ReadLine(), out var price))
                        {
                            form.Price = price;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for product price. Keeping the current value.");
                            form.Price = existingProduct.ProductPrice;
                        }

                        Console.Write("Payment (Leave blank to keep the current value '{0}'): ", existingProduct.MoneyPayment.Payment);
                        form.MoneyPayment = Console.ReadLine() ?? existingProduct.MoneyPayment.Payment;

                        var result = await _productService.UpdateProductAsync(productId, form);

                        if (result)
                        {
                            Console.WriteLine("Product was updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Product with ID {productId} could not be updated.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Product with ID {productId} not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for product ID.");
                }

                Console.ReadKey();
            }

        }
        public async Task DeleteAsync()
        {
            Console.Clear();
            Console.Write("Enter the ID of the product to delete: ");

            if (int.TryParse(Console.ReadLine(), out var productId))
            {
                // Call the service to delete the product
                var result = await _productService.DeleteProductAsync(productId);

                if (result)
                {
                    Console.WriteLine("Product was deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Product with ID {productId} could not be deleted.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for product ID.");
            }

            Console.ReadKey();
        }
    }
}
    

