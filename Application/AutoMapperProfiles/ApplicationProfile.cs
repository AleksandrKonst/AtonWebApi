using Application.DTO;
using AutoMapper;
using Domain.Entity;

namespace Application.AutoMapperProfiles;

/// <summary>
/// Конфигурация маппинга
/// </summary>
public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<NewUserDto, User>()
            .ForMember(p => p.Guid,
                opt => opt.MapFrom(x => Guid.NewGuid()))
            .ForMember(p => p.CreatedOn,
                opt => opt.MapFrom(x => DateTime.Now.ToUniversalTime()))
            .ReverseMap();
        
        CreateMap<NewUserDataDto, User>()
            .ForMember(p => p.ModifiedOn,
            opt => opt.MapFrom(x => DateTime.Now.ToUniversalTime()))
            .ReverseMap();
        
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, UserByLoginDto>().ForMember(p => p.Status,
            opt => opt.MapFrom(x => x.RevokedBy == null))
            .ReverseMap();
    }
}