using AutoMapper;
using PhotoApp.Domain.Entities;
using PhotoApp.Domain.Request;

namespace PhotoApp.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            this.CreateMap<UserEntity, RegisterRequest>().ReverseMap();
            this.CreateMap<PhotoEntity, PhotoRequest>().ReverseMap();
        }
    }
}
