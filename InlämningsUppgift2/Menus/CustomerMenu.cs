using InlämningsUppgift2.Models;
using InlämningsUppgift2.Services;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Menus
{
    internal class CustomerMenu
    {
        private readonly CustomerService _customerService;

        public CustomerMenu(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task ManageCustomer()
        {
            Console.Clear();
            Console.WriteLine("Manage Customers");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. View Customers");
            Console.WriteLine("3. Update Customers");
            Console.WriteLine("4. Delete Customers");
            Console.WriteLine("5. Go back");




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
            var form = new CustomerRegistrationForm();
     
            Console.Clear();
           Console.Write("First Name: ");
           form.FirstName = Console.ReadLine()!;

           Console.Write("Last Name: ");
           form.LastName = Console.ReadLine()!;
          
           Console.Write("Email: ");
           form.Email = Console.ReadLine()!;

            Console.Write("Street Name: ");
            form.StreetName = Console.ReadLine()!;

            Console.Write("Postal Code: ");
            form.PostalCode = Console.ReadLine()!;

            Console.Write("City: ");
            form.City = Console.ReadLine()!;


            var result = await _customerService.CreateCustomerAsync(form);
            if (result)
                Console.WriteLine("--------------------------------------\nCustomer was created\nPress any key to return");
           

            else
                Console.WriteLine("---------------------\nCustomer could not be created");

            Console.ReadKey();
          
        }

        public async Task ListAllAsync()
        {
            var customers = await _customerService.GetAllAsync();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer Id: {customer.Id}\nCustomer Name{customer.FirstName}  {customer.LastName}");
                Console.WriteLine($"Address: {customer.Address.StreetName}\n PostalCode: {customer.Address.PostalCode}\nCity: {customer.Address.City}");
                Console.WriteLine("");

            }
           
            Console.ReadKey();
        }

        public async Task UpdateAsync() 
        {
            Console.Clear();
            Console.Write("Enter the ID of the customer to update: ");

            if (int.TryParse(Console.ReadLine(), out var customerId))
            {
                var existingCustomer = await _customerService.GetCustomerByIdAsync(customerId);

                if (existingCustomer != null)
                {
                    var form = new CustomerRegistrationForm();

                    Console.Write("First Name (Leave blank to keep the current value '{0}'): ", existingCustomer.FirstName);
                    form.FirstName = Console.ReadLine() ?? existingCustomer.FirstName;

                    Console.Write("Last Name (Leave blank to keep the current value '{0}'): ", existingCustomer.LastName);
                    form.LastName = Console.ReadLine() ?? existingCustomer.LastName;

                    Console.Write("Email (Leave blank to keep the current value '{0}'): ", existingCustomer.Email);
                    form.Email = Console.ReadLine() ?? existingCustomer.Email;

                    Console.Write("Street Name (Leave blank to keep the current value '{0}'): ", existingCustomer.Address.StreetName);
                    form.StreetName = Console.ReadLine() ?? existingCustomer.Address.StreetName;

                    Console.Write("Postal Code (Leave blank to keep the current value '{0}'): ", existingCustomer.Address.PostalCode);
                    form.PostalCode = Console.ReadLine() ?? existingCustomer.Address.PostalCode;

                    Console.Write("City (Leave blank to keep the current value '{0}'): ", existingCustomer.Address.City);
                    form.City = Console.ReadLine() ?? existingCustomer.Address.City;

                    var result = await _customerService.UpdateCustomerAsync(customerId, form);

                    if (result)
                    {
                        Console.WriteLine("Customer was updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Customer with ID {customerId} could not be updated.");
                    }
                }
                else
                {
                    Console.WriteLine($"Customer with ID {customerId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for customer ID.");
            }

            Console.ReadKey();
        }

        public async Task DeleteAsync()
        {
            Console.Clear();
            Console.Write("Enter the ID of the customer to delete: ");

            if (int.TryParse(Console.ReadLine(), out var customerId))
            {
                // Call the service to delete the customer
                var result = await _customerService.DeleteAsync(customerId);

                if (result)
                {
                    Console.WriteLine("Customer was deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Customer with ID {customerId} could not be deleted.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for customer ID.");
            }
            Console.ReadKey();
        }
    }
}


    

