using AutoMapper;
using Api.Database.Dtos.People;
using Api.Database.Entities.People;

namespace Api.Profiles.People
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
