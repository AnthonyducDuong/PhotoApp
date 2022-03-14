using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Enums;
using PhotoApp.Domain.Interfaces.IRepositories;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Response;
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

        public override async Task<bool> Delete(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return false;
                }

                CommentEntity commentEntity = this.dbSet.FirstOrDefault(c => c.Id.Equals(Guid.Parse(Id)));

                if (commentEntity == null)
                {
                    return false;
                }

                if (commentEntity.commentEntities.Count() == 0)
                {
                    this.dbSet.Remove(commentEntity);
                }
                else
                {
                    this.remove(commentEntity);
                }
                //this.remove(commentEntity);

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't delete comment, Error Message = {ex.Message}");
                throw;
            }
        }

        /*public override Task<CommentEntity?> GetById(string Id)
        {
            CommentEntity commentExisted = this.dbSet.FirstOrDefault(c => c.Id.Equals(Guid.Parse(Id)));

            if (commentExisted == null)
            {

            }
        }*/

        private void remove(CommentEntity commentEntity)
        {
            foreach (var item in commentEntity.commentEntities)
            {
                if (item.commentEntities.Count() == 0)
                {
                    this.dbSet.Remove(item);
                }
                else
                {
                    remove(item); 
                }
            }
            this.dbSet.Remove(commentEntity);
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

        public async Task<NormalResponse> updateCommentAsync(CommentUpdateRequest request)
        {
            try
            {
                if (request == null)
                {
                    return new NormalResponse
                    {
                        Success = false,
                        Message = "request invalid",
                    };
                }

                CommentEntity commentEntity = this.dbSet.FirstOrDefault(c => c.Id.Equals(Guid.Parse(request.Id)));

                if (commentEntity == null)
                {
                    return new NormalResponse
                    {
                        Success = false,
                        Message = "request invalid",
                    };
                }

                commentEntity.Content = request.Content;
                commentEntity.Status = StatusCommentEnums.EDITED;

                this.dbSet.Update(commentEntity);

                return new NormalResponse
                {
                    Success = true,
                    Message = "Update comment successfully!",
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't update new comment, Error Message = {ex.Message}");
                throw;
            }
        }
    }
}
