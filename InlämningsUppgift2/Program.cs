using InlämningsUppgift2.Context;
using InlämningsUppgift2.Menus;
using InlämningsUppgift2.Repositories;
using InlämningsUppgift2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InlämningsUppgift2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
           
            
            services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\basch\Desktop\Skola\Databasteknik\Inlämningsuppgift2\Inlämningsuppgift2\InlämningsUppgift2\InlämningsUppgift2\Context\Inlämningsuppgift.mdf;Integrated Security=True;Connect Timeout=30"));

            //Repositories
            services.AddScoped<AddressRepository>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<MoneyPaymentRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductCategoryRepository>();





            //Services
            services.AddScoped<CustomerService>();
            services.AddScoped<ProductService>();




            //Menus
            services.AddScoped<CustomerMenu>();
            services.AddScoped<MainMenu>();
            services.AddScoped<ProductMenu>();








            var sp = services.BuildServiceProvider();
            var mainMenu = sp.GetRequiredService<MainMenu>();
            await mainMenu.StartAsync();

        }
    }
}