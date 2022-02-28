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
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel> 
        where TEntity : class 
        where TModel : class
    {
        protected ApplicationDbContext _applicationDbContext;
        protected readonly ILogger _logger;
        internal DbSet<TEntity> dbSet;
        protected readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext applicationDbContext, ILogger logger
            , IMapper mapper)
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

        public virtual async Task<bool> Add(TModel model)
        {
            var entity = this._mapper.Map<TEntity>(model);
            await this.dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<TModel>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await this.dbSet.Where(predicate).ToListAsync();
            return this._mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<IEnumerable<TModel>> All()
        {
            var entities = await this.dbSet.ToListAsync().ConfigureAwait(false);
            return this._mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<TModel?> GetById(Guid Id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            TEntity entity = await this.dbSet.FindAsync(Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return this._mapper.Map<TModel>(entity);
        }

        public virtual Task<bool> Update(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
