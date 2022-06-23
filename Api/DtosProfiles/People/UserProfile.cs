using AutoMapper;
using Domain.Dtos.People;
using Domain.Entities.People;

namespace Api.DtosProfiles.People
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
