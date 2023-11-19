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
    internal class ProductRepository : Repo<ProductEntity>
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.MoneyPayment).Include(x => x.ProductCategory).ToListAsync();
        }


        //  public async Task<bool> UpdateAsync(ProductEntity updatedProduct)
        //{

        // }

        public async Task<bool> DeleteAsync(ProductEntity entity)
        {
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false;
        }
        public async Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            return await _context.Products.AnyAsync(expression);
        }

        public async Task<ProductEntity> GetAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.MoneyPayment)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<bool> UpdateAsync(ProductEntity product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or rethrow if needed)
                Console.WriteLine($"Error updating product: {ex.Message}");
                return false;
            }
        }
    }
}
