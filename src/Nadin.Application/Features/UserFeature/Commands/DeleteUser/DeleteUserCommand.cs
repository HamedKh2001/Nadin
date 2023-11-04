using MediatR;

namespace Nadin.Application.Features.UserFeature.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
    }
}
