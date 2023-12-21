using Microsoft.AspNetCore.Authorization;
using Nadin.Domain.Enums;

namespace Nadin.WebAPI.ABAC.Requirements
{
    public class GenderRequirement : IAuthorizationRequirement
    {
        public GenderRequirement(GenderType requiredGender)
        {
            RequiredGender = requiredGender;
        }

        public GenderType RequiredGender { get; }
    }
}
