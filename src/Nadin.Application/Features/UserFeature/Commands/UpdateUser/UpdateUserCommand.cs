using MediatR;
using Nadin.Domain.Enums;
using System;

namespace Nadin.Application.Features.UserFeature.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }
        public NationalType NationalType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public GenderType Gender { get; set; }
    }
}
