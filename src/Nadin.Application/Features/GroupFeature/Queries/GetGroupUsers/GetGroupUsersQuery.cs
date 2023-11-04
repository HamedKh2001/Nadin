using MediatR;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using SharedKernel.Common;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroupUsers
{
    public class GetGroupUsersQuery : PaginationQuery, IRequest<PaginatedList<UserDto>>
    {
        public int GroupId { get; set; }
        public string SearchParam { get; set; }
    }
}
