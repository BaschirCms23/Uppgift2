using InlämningsUppgift2.Entities;
using InlämningsUppgift2.Models;
using InlämningsUppgift2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Services
{
    internal class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly AddressRepository _AddressRepository;

        public CustomerService(CustomerRepository customerRepository, AddressRepository addressRepository)
        {
            _customerRepository = customerRepository;
            _AddressRepository = addressRepository;
        }

        public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
        {
            //Check Customer
            if(!await _customerRepository.ExistsAsync(x => x.Email == form.Email))
            {
                // Check Address
                AddressEntity addressEntity = await _AddressRepository.GetAsync(x => x.StreetName == form.StreetName && x.PostalCode == form.PostalCode);
                addressEntity ??= await _AddressRepository.CreateAsync(new AddressEntity { StreetName = form.StreetName, PostalCode = form.PostalCode, City = form.City });

                // Create Customer
                CustomerEntity customerEntity = await _customerRepository.CreateAsync(new CustomerEntity { FirstName = form.FirstName, LastName = form.LastName, Email = form.Email, AddressId = addressEntity.Id});
                if(customerEntity != null) 
                    return true;
            }
            return false;
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers;
        }

       public async Task<bool> DeleteAsync(int customerId)
    {
        try
        {
            // Ensure the customer with the specified ID exists
            var existingCustomer = await _customerRepository.GetAsync(customerId);

            if (existingCustomer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found for deletion.");
                return false;
            }

            // Call the repository to delete the customer
            return await _customerRepository.DeleteAsync(existingCustomer);
        }
        catch (Exception ex)
        {
            // Handle exceptions (log or rethrow if needed)
            Console.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }

        public async Task<CustomerEntity> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetAsync(customerId);
        }
        public async Task<bool> UpdateCustomerAsync(int customerId, CustomerRegistrationForm form)
        {
            var existingCustomer = await _customerRepository.GetAsync(customerId);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = form.FirstName;
                existingCustomer.LastName = form.LastName;
                existingCustomer.Email = form.Email;
                existingCustomer.Address.StreetName = form.StreetName;
                existingCustomer.Address.PostalCode = form.PostalCode;
                existingCustomer.Address.City = form.City;

                return await _customerRepository.UpdateAsync(existingCustomer);
            }

            return false;
        }



    }
}
    



