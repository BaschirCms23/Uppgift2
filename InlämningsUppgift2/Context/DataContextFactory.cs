using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Context
{
    internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\basch\Desktop\Skola\Databasteknik\Inlämningsuppgift2\Inlämningsuppgift2\InlämningsUppgift2\InlämningsUppgift2\Context\Inlämningsuppgift.mdf;Integrated Security=True;Connect Timeout=30");
            return new DataContext(optionsBuilder.Options);
        }
    }
}
