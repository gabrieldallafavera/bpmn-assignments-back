using AutoMapper;
using Api.Database.Entities.People;
using Api.Models.People;

namespace Api.Profiles.People
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserRolesRequest, UserRoles>();
            CreateMap<RefreshTokenResponse, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenResponse>();
        }
    }
}
