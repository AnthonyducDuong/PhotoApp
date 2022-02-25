using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories.IGeneric
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T entity);

        Task<bool> Delete(Guid Id);

        Task<bool> Update(T entity);

        Task<IEnumerable<T>> All();

        Task<T?> GetById(Guid Id);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
