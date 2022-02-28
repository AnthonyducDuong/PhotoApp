using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories.IGeneric
{
    public interface IGenericRepository<TEntity, TModel> where TEntity : class where TModel : class
    {
        Task<bool> Add(TModel model);

        Task<bool> Delete(Guid Id);

        Task<bool> Update(TModel model);

        Task<IEnumerable<TModel>> All();

        Task<TModel?> GetById(Guid Id);

        Task<IEnumerable<TModel>> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
