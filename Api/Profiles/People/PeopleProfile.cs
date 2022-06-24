using AutoMapper;
using Api.Database.Dtos.People;
using Api.Database.Entities.People;

namespace Api.Profiles.People
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<UserWriteDto, User>();
            CreateMap<User, UserReadDto>();
            CreateMap<RefreshTokenDto, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenDto>();
        }
    }
}
