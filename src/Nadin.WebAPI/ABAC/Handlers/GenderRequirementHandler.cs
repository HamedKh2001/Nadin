using Microsoft.AspNetCore.Authorization;
using Nadin.Domain.Enums;
using Nadin.WebAPI.ABAC.Requirements;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nadin.WebAPI.ABAC.Handlers
{
    public class GenderRequirementHandler : AuthorizationHandler<GenderRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GenderRequirement requirement)
        {
            var isParsed = Enum.TryParse(context.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Gender).Value, out GenderType gender);
            // Perform access control logic based on attributes
            if (isParsed && gender == requirement.RequiredGender)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        //private bool IsAuthorized(ClaimsPrincipal user)
        //{
        //    return user.HasClaim(c => c.Type == "IsAdmin" && c.Value == "true");
        //}
    }
}
