using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfrastructureLayer.Context;
using DomainLayer.Repositories;

namespace InfrastructureLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FinanceAppDbContext _context;
        private IDbContextTransaction _transaction;

        public IAccountRepository AccountRepository { get; }
        public ITransactionRepository TransactionRepository { get; }

        public UnitOfWork(
            FinanceAppDbContext context,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _context = context;
            AccountRepository = accountRepository;
            TransactionRepository = transactionRepository;
        }


        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                if (_transaction != null)
                    await _transaction.CommitAsync();
            }
            catch
            {
                if (_transaction != null)
                    await _transaction?.RollbackAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
