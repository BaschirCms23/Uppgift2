using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Entities
{
    internal class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }
        public int MoneyPaymentId { get; set; }
        public MoneyPaymentEntity MoneyPayment { get; set; } = null!;

        public int ProductCategoryId { get; set; }
        public ProductCategoryEntity ProductCategory { get; set; } = null!;
    }
}
