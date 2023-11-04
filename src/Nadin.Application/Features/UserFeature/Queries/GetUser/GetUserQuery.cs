using MediatR;

namespace Nadin.Application.Features.UserFeature.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
