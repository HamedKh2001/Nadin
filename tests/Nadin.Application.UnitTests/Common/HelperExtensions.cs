using AutoMapper;
using Nadin.Application.Features.RoleFeature.Queries.GetRoleUsers;
using Nadin.Application.Features.UserFeature.Commands.CreateUser;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using Nadin.Domain.Entities;
using SharedKernel.Extensions;

namespace Nadin.Application.UnitTests.Common
{
    public static class HelperExtensions
    {
        public static IMapper GetMapper(this IMapper mapper)
        {
            var configurationProvider = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateUserCommand, User>()
                        .ForMember(u => u.UserName, opt => opt.MapFrom(src => src.UserName.ToLower()))
                        .ForMember(u => u.FirstName, opt => opt.MapFrom(src => src.FirstName.ClearArabicCharacter()))
                        .ForMember(u => u.LastName, opt => opt.MapFrom(src => src.LastName.ClearArabicCharacter()))
                        .ForMember(u => u.Mobile, opt => opt.MapFrom(src => src.Mobile.ClearArabicCharacter()));

                    cfg.CreateMap<Group, GetRoleUsersDto>()
                        .ForMember(g => g.GroupId, opt => opt.MapFrom(src => src.Id))
                        .ForMember(g => g.GroupCaption, opt => opt.MapFrom(src => src.Caption));

                    cfg.CreateMap<User, UserDto>()
                        .ForMember(u => u.Gender, opt => opt.MapFrom(src => (byte)src.Gender));
                });

            return configurationProvider.CreateMapper();
        }
    }
}
