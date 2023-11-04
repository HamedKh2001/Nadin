using MediatR;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using SharedKernel.Common;

namespace Nadin.Application.Features.UserFeature.Queries.GetUsers
{
    public class GetUsersQuery : PaginationQuery, IRequest<PaginatedList<UserDto>>
    {
        public string SearchParam { get; set; }
    }
}
