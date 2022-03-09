using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoApp.Domain.Entities;
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
    public class PhotoRepository : GenericRepository<PhotoEntity>, IPhotoRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public PhotoRepository(ApplicationDbContext applicationDbContext, ILogger logger, IMapper mapper
            , UserManager<UserEntity> userManager) :
            base(applicationDbContext, logger, mapper)
        {
            this._userManager = userManager;
        }

        // Coi lại có í tưởng xử lý
        public override async Task<IEnumerable<PhotoEntity>> All()
        {
            try
            {
                IEnumerable<PhotoEntity> photoEntities = (from photo in await this.dbSet.ToListAsync().ConfigureAwait(false)
                                                          select new PhotoEntity
                                                          (
                                                              photo.Id,
                                                              photo.Description,
                                                              photo.Url,
                                                              photo.Mode,
                                                              photo.CreatedAt,
                                                              photo.Updated,
                                                              photo.likePhotoEntities,
                                                              photo.dislikePhotoEntities,
                                                              photo.commentEntities,
                                                              photo.userEntity = new UserEntity(
                                                                       photo.userEntity.Id,
                                                                       photo.userEntity.FirstName,
                                                                       photo.userEntity.LastName,
                                                                       photo.userEntity.Gender,
                                                                       photo.userEntity.UserName,
                                                                       photo.userEntity.Email
                                                                  )
                                                          )).ToList();

                //IEnumerable<PhotoEntity> photoEntities = this.dbSet.FromSqlInterpolated($"select Photo.Id, Photo.Description, Photo.Url, Photo.Mode, Photo.CreatedAt, Photo.Updated from Photo");
                return photoEntities;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't get all photos, Error Message = {ex.Message}");
                throw;
            }
        }

        public override async Task<bool> Delete(string Id)
        {
            try
            {
                if (String.IsNullOrEmpty(Id))
                {
                    return false;
                }

                PhotoEntity photoEntity = await this.dbSet.FirstOrDefaultAsync(p => p.Id.Equals(Guid.Parse(Id)));

                if (photoEntity == null)
                {
                    return false;
                }

                var delete = this.dbSet.Remove(photoEntity);

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't delete photo with id = {Id}, Error Message = {ex.Message}");
                throw;
            }
        }

        public async Task<Response<PhotoEntity>> createPhotoAsync(PhotoRequest request)
        {
            try
            {
                if (request == null)
                {
                    return new Response<PhotoEntity>()
                    {
                        Success = false,
                        Message = "Request invalid",
                    };
                }

                PhotoEntity photoEntity = this._mapper.Map<PhotoEntity>(request);
                await this.Add(photoEntity);
                return new Response<PhotoEntity>()
                {
                    Success = true,
                    Message = "Create photo successfully",
                    Data = photoEntity,
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't create new photo, Error Message = {ex.Message}");
                throw;
            }
        }

        public async Task<Response<IEnumerable<PhotoEntity>>> getAllPhotosOfUser(string userId)
        {
           try
            {
                if (String.IsNullOrEmpty(userId))
                {
                    return new Response<IEnumerable<PhotoEntity>>()
                    {
                        Success = false,
                        Message = "UserId invalid",
                    };
                }

                UserEntity userEntity = await this._userManager.FindByIdAsync(userId);

                if (userEntity == null)
                {
                    return new Response<IEnumerable<PhotoEntity>>()
                    {
                        Success = false,
                        Message = "UserId invalid",
                    };
                }

                //IEnumerable<PhotoEntity> photoEntities = await this.dbSet.Where(u => u.UserId.Equals(userEntity.Id)).ToListAsync();

                //IEnumerable<PhotoEntity> photoEntities = from photo in this.dbSet.Where(u => u.UserId.Equals(userEntity.Id))
                //select photo;

                /*IEnumerable<PhotoEntity> photoEntities = await (from photo in this.dbSet.Where(u => u.UserId.Equals(userEntity.Id)) 
                                                         select new PhotoEntity { 
                                                            Id = photo.Id,
                                                            Description = photo.Description,
                                                            Url = photo.Url,
                                                            Mode = photo.Mode,
                                                            CreatedAt = photo.CreatedAt,
                                                            Updated = photo.Updated,
                                                            likePhotoEntities = photo.likePhotoEntities,
                                                            dislikePhotoEntities = photo.dislikePhotoEntities,
                                                            commentEntities = photo.commentEntities,
                                                         }).ToListAsync();*/

#pragma warning disable CS8604 // Possible null reference argument.
                IEnumerable<PhotoEntity> photoEntities = await (from photo in this.dbSet.Where(u => u.UserId.Equals(userEntity.Id))
                                                       select new PhotoEntity(photo.Id, photo.Description, photo.Url, photo.Mode, photo.CreatedAt, photo.Updated
                                                       , photo.likePhotoEntities, photo.dislikePhotoEntities, photo.commentEntities)).ToListAsync();
#pragma warning restore CS8604 // Possible null reference argument.

                return new Response<IEnumerable<PhotoEntity>>()
                {
                    Success = true,
                    Message = "get all photos of user successfully!",
                    Data = photoEntities,
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't get all photos of user, Error Message = {ex.Message}");
                throw;
            }
        }

        public async Task<NormalResponse> updatePhotoAsync(PhotoRequest request)
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

                PhotoEntity photoEntity = await this.dbSet.FirstOrDefaultAsync(p => p.Id.Equals(Guid.Parse(request.Id)));

                if (photoEntity == null)
                {
                    return new NormalResponse
                    {
                        Success = false,
                        Message = "photo id invalid",
                    };
                }

                photoEntity.Description = request.Description;
                photoEntity.Url = request.Url;
                photoEntity.Mode = request.Mode;
                photoEntity.Updated = DateTime.UtcNow;

                var result = this.dbSet.Update(photoEntity);

                return new NormalResponse
                {
                    Success = true,
                    Message = "update successfully!",
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Can't create new photo, Error Message = {ex.Message}");
                throw;
            }
        }
    }
}
