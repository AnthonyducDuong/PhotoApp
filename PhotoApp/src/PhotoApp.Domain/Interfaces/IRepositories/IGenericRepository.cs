using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid Id);
        Task<bool> Update(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetById(Guid Id);
    }
}
