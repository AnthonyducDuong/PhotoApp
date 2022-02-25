using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IRepositories.IGeneric;
using PhotoApp.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _applicationDbContext;
        protected readonly ILogger _logger;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext applicationDbContext, ILogger logger)
        {
            this._applicationDbContext = applicationDbContext;
            this._logger = logger;
            this.dbSet = applicationDbContext.Set<T>();
        }

        public virtual async Task<bool> Add(T entity)
        {
            await this.dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await this.dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await this.dbSet.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<T?> GetById(Guid Id)
        {
            return await this.dbSet.FindAsync(Id);
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
