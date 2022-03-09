using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IRepositories.IGenericRepository;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories
{
    public interface ICommentRepository : IGenericRepository<CommentEntity>
    {
        Task<Response<CommentRequest>> createCommentAsync(CommentRequest request);
    }
}
