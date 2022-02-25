using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Models;
using PhotoApp.Infrastructure.Contexts;
using PhotoApp.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext, ILogger logger) : base(applicationDbContext, logger) { }

        // Override
        public override async Task<IEnumerable<UserModel>> All()
        {
            try
            {
                return await this.dbSet.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<UserModel>();
            }
        }

        public override async Task<bool> Update(UserModel entity)
        {
            try
            {
                var existingUser = await this.dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.UserName = entity.UserName;
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                existingUser.Password = entity.Password;

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} Update function error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid Id)
        {
            try
            {
                var exist = await this.dbSet.Where(x => x.Id == Id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                this.dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }

        public Task<UserModel> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
