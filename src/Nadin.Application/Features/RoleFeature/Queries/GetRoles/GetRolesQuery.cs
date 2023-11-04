using MediatR;
using System.Collections.Generic;

namespace Nadin.Application.Features.RoleFeature.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<RoleDto>>
    {
    }
}
