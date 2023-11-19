using InlämningsUppgift2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Context
{
    internal class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductCategoryEntity> Category { get; set; }
        public DbSet<MoneyPaymentEntity> Payment { get; set; }


    }
}
