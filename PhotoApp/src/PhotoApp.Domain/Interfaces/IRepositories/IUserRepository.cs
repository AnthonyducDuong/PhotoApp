
using PhotoApp.Domain.Interfaces.IRepositories.IGeneric;
using PhotoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        // Get user by email
        Task<UserModel> GetUserByEmail(string email);
    }
}
