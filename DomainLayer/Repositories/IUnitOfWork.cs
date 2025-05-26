using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        Task CommitAsync();
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
