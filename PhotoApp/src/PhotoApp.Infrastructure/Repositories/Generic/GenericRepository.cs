using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IRepositories.IGeneric;
using PhotoApp.Infrastructure.Configuration;
using PhotoApp.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Repositories.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class 
    {
        protected ApplicationDbContext _applicationDbContext;
        protected readonly ILogger _logger;
        internal DbSet<TEntity> dbSet;
        protected readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext applicationDbContext, ILogger logger, IMapper mapper)
        {
            /*if (applicationDbContext == null)
            {
                throw new ApplicationException("DbContext cannot be null.");
            }*/

            this._applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbSet = applicationDbContext.Set<TEntity>();
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            await this.dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> All()
        {
            return await this.dbSet.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity?> GetById(Guid Id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            return await this.dbSet.FindAsync(Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        public virtual Task<bool> Update(TEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
