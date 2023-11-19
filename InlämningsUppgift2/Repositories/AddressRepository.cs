using InlämningsUppgift2.Context;
using InlämningsUppgift2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Repositories
{
    internal class AddressRepository : Repo<AddressEntity>
    {
        public AddressRepository(DataContext context) : base(context)
        {
        }
    }
}
