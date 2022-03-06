using PhotoApp.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository userRepository { get; }
        IPhotoRepository photoRepository { get; }

        Task CompleteAsync();

        void Dispose();
    }
}
