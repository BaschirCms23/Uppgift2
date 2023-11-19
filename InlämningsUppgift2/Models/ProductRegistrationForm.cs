using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Models
{
    internal class ProductRegistrationForm
    {
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal Price { get; set; }
        public string MoneyPayment { get; set; } = null!;

        public string ProductCategory { get; set; } = null!;
    }
}
