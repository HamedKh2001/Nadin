using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Nadin.WebAPI.CBAC.AccessContext;
using System.Threading.Tasks;

namespace Nadin.WebAPI.CBAC.Handlers
{
    public class IPAccessControlHandler : AuthorizationHandler<IPAccessControlContext>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IPAccessControlHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IPAccessControlContext requirement)
        {
            var userIP = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            foreach (var ip in requirement.ValidIPs)
            {
                if (userIP == ip)
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
