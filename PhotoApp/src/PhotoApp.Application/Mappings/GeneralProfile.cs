using AutoMapper;
using PhotoApp.Domain.Models;
using PhotoApp.Infrastructure.Entities;

namespace PhotoApp.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
    }
}
