using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.DTO;
using AutoMapper;

namespace Abdt.Loyal.UserManager.Configuration
{
    public class ControllersMappingProfile : Profile
    {
        public ControllersMappingProfile()
        {
            CreateMap<User, UserDtoRegister>();
            CreateMap<UserDtoRegister, User>();

            CreateMap<User, UserDtoUpdate>();
            CreateMap<UserDtoUpdate, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
