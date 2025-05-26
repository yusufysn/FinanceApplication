using DomainLayer.Entities;
using DomainLayer.Repositories;
using InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(FinanceAppDbContext context) : base(context)
        {
        }
    }
}
