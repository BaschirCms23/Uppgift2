using InlämningsUppgift2.Context;
using InlämningsUppgift2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Repositories
{
    internal class MoneyPaymentRepository : Repo<MoneyPaymentEntity>
    {
        public MoneyPaymentRepository(DataContext context) : base(context)
        {
        }
    }
}
