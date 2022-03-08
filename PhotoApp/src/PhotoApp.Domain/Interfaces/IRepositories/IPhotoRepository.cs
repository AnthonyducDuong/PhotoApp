using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Interfaces.IRepositories.IGeneric;
using PhotoApp.Domain.Request;
using PhotoApp.Domain.Response;
using PhotoApp.Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IRepositories
{
    public interface IPhotoRepository : IGenericRepository<PhotoEntity>
    {
        // User create new photo
        public Task<Response<PhotoEntity>> createPhotoAsync(PhotoRequest request);

        // Get all photos of user
        public Task<Response<IEnumerable<PhotoEntity>>> getAllPhotosOfUser(string userId);

        // Update 
        public Task<NormalResponse> updatePhotoAsync(PhotoRequest request);
    }
}
