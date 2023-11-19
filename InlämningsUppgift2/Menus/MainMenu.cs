using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Menus
{
    internal class MainMenu
    {
        private readonly CustomerMenu _customerMenu;
        private readonly ProductMenu _productMenu;

        public MainMenu(CustomerMenu customerMenu, ProductMenu productMenu)
        {
            _customerMenu = customerMenu;
            _productMenu = productMenu;
        }

        public async Task StartAsync()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Manage Customer");
                Console.WriteLine("2. Manage Products");
                Console.WriteLine("0. Exit ");


                Console.Write("Choose one option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await _customerMenu.ManageCustomer();
                        break;
                    case "2":
                        await _productMenu.ManageProducts();
                        break;
                    case "0":
                         Environment.Exit(0);
                        break;
                }

            }
            while (true);
        }
     
      
    }
}
