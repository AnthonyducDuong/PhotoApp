using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories.IGeneric
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> Add(TEntity entity);

        Task<bool> Delete(Guid Id);

        Task<bool> Update(TEntity entity);

        Task<IEnumerable<TEntity>> All();

        Task<TEntity?> GetById(Guid Id);

        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
