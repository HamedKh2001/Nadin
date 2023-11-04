using MediatR;
using System.Collections.Generic;

namespace Nadin.Application.Features.RoleFeature.Queries.GetRoleUsers
{
    public class GetRoleUsersQuery : IRequest<List<GetRoleUsersDto>>
    {
        public int RoleId { get; set; }
    }
}
