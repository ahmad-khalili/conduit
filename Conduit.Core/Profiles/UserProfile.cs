using AutoMapper;
using Conduit.Core.Entities;
using Conduit.Core.Models;

namespace Conduit.Core.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserForCreationDto, User>();
        CreateMap<User, UserDto>();
    }
}