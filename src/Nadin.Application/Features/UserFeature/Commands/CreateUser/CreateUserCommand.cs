using MediatR;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using Nadin.Domain.Enums;

namespace Nadin.Application.Features.UserFeature.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public NationalType NationalType { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public GenderType Gender { get; set; }
    }
}
