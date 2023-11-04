using AutoMapper;
using Nadin.Application.Features.GroupFeature.Commands.UpdateGroup;
using Nadin.Application.Features.GroupFeature.Queries.GetGroup;
using Nadin.Application.Features.GroupFeature.Queries.GetGroupRoles;
using Nadin.Application.Features.ProductFeature.Commands.CreateProduct;
using Nadin.Application.Features.ProductFeature.Commands.DeleteProduct;
using Nadin.Application.Features.ProductFeature.Commands.UpdateProduct;
using Nadin.Application.Features.ProductFeature.Queries.GetProduct;
using Nadin.Application.Features.RoleFeature.Commands.UpdateRole;
using Nadin.Application.Features.RoleFeature.Queries.GetRoleGroups;
using Nadin.Application.Features.RoleFeature.Queries.GetRoles;
using Nadin.Application.Features.RoleFeature.Queries.GetRoleUsers;
using Nadin.Application.Features.UserFeature.Commands.CreateUser;
using Nadin.Application.Features.UserFeature.Commands.UpdateUser;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using Nadin.Application.Mapping.Resolver.ProductResolver;
using Nadin.Domain.Entities;
using SharedKernel.Common;

namespace Nadin.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(PaginatedList<>));
            CreateMap<DateOnly, DateTime>().ConvertUsing(input => input.ToDateTime(TimeOnly.Parse("00:00 AM")));
            CreateMap<DateOnly?, DateTime?>().ConvertUsing(input => input.HasValue ? input.Value.ToDateTime(TimeOnly.Parse("00:00 AM")) : null);
            CreateMap<DateTime, DateOnly>().ConvertUsing(input => DateOnly.FromDateTime(input));
            CreateMap<DateTime?, DateOnly?>().ConvertUsing(input => input.HasValue ? DateOnly.FromDateTime(input.Value) : null);

            CreateMap<Group, GroupDto>();
            CreateMap<Group, GroupRolesDto>();
            CreateMap<UpdateGroupCommand, Group>();
            CreateMap<Group, GetRoleUsersDto>()
                .ForMember(g => g.GroupId, opt => opt.MapFrom(src => src.Id))
                .ForMember(g => g.GroupCaption, opt => opt.MapFrom(src => src.Caption));
            CreateMap<Group, GetRoleGroupsDto>()
                .ForMember(g => g.GroupId, opt => opt.MapFrom(src => src.Id))
                .ForMember(g => g.GroupCaption, opt => opt.MapFrom(src => src.Caption));

            CreateMap<Role, RoleDto>();
            CreateMap<UpdateRoleCommand, Role>();

            CreateMap<User, UserDto>()
                .ForMember(u => u.Gender, opt => opt.MapFrom(src => (byte)src.Gender));
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductCommand, Product>()
                .ForMember(p => p.UserId, opt => opt.MapFrom<CreateProductCommandResolver>());
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<DeleteProductCommand, Product>();
        }
    }
}
