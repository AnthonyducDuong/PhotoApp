using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class PhotoRepository : GenericRepository<PhotoEntity>, IPhotoRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public PhotoRepository(ApplicationDbContext applicationDbContext, ILogger logger, IMapper mapper
            , UserManager<UserEntity> userManager) :
            base(applicationDbContext, logger, mapper)
        {
            this._userManager = userManager;
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

        public async Task<Response<IEnumerable<PhotoEntity>>> getAllPhotosOfUser(string email)
        {
           try
            {
                UserEntity userEntity = await this._userManager.FindByEmailAsync(email);

                if (userEntity == null || String.IsNullOrEmpty(email))
                {
                    return new Response<IEnumerable<PhotoEntity>>()
                    {
                        Success = false,
                        Message = "UserId invalid",
                    };
                }

                //IEnumerable<PhotoEntity> photoEntities = await this.dbSet.Where(u => u.UserId.Equals(userEntity.Id)).ToListAsync();

                IEnumerable<PhotoEntity> photoEntities = await this.dbSet.IgnoreAutoIncludes().ToListAsync();

                foreach (var item in photoEntities)
                {
                    System.Console.WriteLine(item);
                }
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
    }
}
