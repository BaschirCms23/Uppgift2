using InlämningsUppgift2.Context;
using InlämningsUppgift2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Repositories
{
    internal class CustomerRepository : Repo<CustomerEntity>
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.Include(x => x.Address).ToListAsync();
        }

        
        
        public async Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            return await _context.Products.AnyAsync(expression);
        }

        public async Task<CustomerEntity> GetAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

   

        public async Task<bool> DeleteAsync(CustomerEntity entity)
        {
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> UpdateAsync(CustomerEntity customer)
        {
            try
            {
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or rethrow if needed)
                Console.WriteLine($"Error updating customer: {ex.Message}");
                return false;
            }
        }

    }
}

