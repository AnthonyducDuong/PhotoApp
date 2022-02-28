using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Models;
using PhotoApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Configuration
{
    public interface IUnitOfWork
    {
        IUserRepository<UserEntity, UserModel> userRepository { get; }

        Task CompleteAsync();

        void Dispose();
    }
}
