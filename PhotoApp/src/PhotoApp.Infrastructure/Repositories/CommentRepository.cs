using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Wrappers;
using PhotoApp.Infrastructure.Contexts;
using PhotoApp.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public CommentRepository(ApplicationDbContext applicationDbContext, ILogger logger, IMapper mapper
            , UserManager<UserEntity> userManager) 
            : base(applicationDbContext, logger, mapper)
        {
            this._userManager = userManager;
        }

        public async Task<Response<CommentRequest>> createCommentAsync(CommentRequest request)
        {
            try
            {
                if (request == null)
                {
                    return new Response<CommentRequest>()
                    {
                        Success = false,
                        Message = "create new comment unsuccessfully",
                    };
                }

                // check UserId
                UserEntity userEntity = await this._userManager.FindByIdAsync(request.UserId);
                if (userEntity == null)
                {
                    return new Response<CommentRequest>()
                    {
                        Success = false,
                        Message = "UserId invalid",
                    };
                }

                // check PhotoId
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                PhotoEntity photoEntity = await this._applicationDbContext.Set<PhotoEntity>().FindAsync(Guid.Parse(request.PhotoId));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (photoEntity == null)
                {
                    return new Response<CommentRequest>()
                    {
                        Success = false,
                        Message = "PhotoId invalid",
                    };
                }

                // check commentId
                if (!String.IsNullOrEmpty(request.CommentId))
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    CommentEntity commentEntityParent = await this.dbSet.FindAsync(Guid.Parse(request.CommentId));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                    if (commentEntityParent == null)
                    {
                        return new Response<CommentRequest>()
                        {
                            Success = false,
                            Message = "CommentId invalid",
                        };
                    }
                }

                CommentEntity commentEntity = this._mapper.Map<CommentEntity>(request);

                var result = await this.dbSet.AddAsync(commentEntity);

                return new Response<CommentRequest>()
                {
                    Success = true,
                    Message = "create new comment successfully",
                    Data = request,
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't create new comment, Error Message = {ex.Message}");
                throw;
            }
        }
    }
}
