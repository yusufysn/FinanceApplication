using DomainLayer.Entities;
using DomainLayer.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Context
{
    public class FinanceAppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected FinanceAppDbContext()
        {
        }
        public FinanceAppDbContext(DbContextOptions<FinanceAppDbContext> options) : base(options) 
        {
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedTime = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedTime = DateTime.Now,
                    _ => DateTime.Now,
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<Account> Accounts {get; set;}
    }
}
