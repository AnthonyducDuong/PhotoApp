
using PhotoApp.Domain.Interfaces.IRepositories.IGeneric;
using PhotoApp.Domain.Models;
using PhotoApp.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories
{
    public interface IUserRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        // Get user by email
        public Task<TModel> GetUserByEmail(string email);

        public Task<Response<TModel>> RegisterUserAsync(TModel model);
    }
}
