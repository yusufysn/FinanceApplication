using DomainLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(Guid Id, bool tracking = true);

        Task<bool> AddAsync(T entities);
        Task<bool> AddRangeAsync(List<T> entity);
        bool Update(T entity);
        bool Remove(T entity);
        Task<bool> RemoveAsync(Guid Id);
        bool RemoveRange(List<T> entities);
        Task<int> SaveAsync();
    }
}
