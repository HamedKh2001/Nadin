using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Nadin.WebAPI.CBAC.AccessContext
{
    public class IPAccessControlContext : IAuthorizationRequirement
    {
        public IPAccessControlContext(List<string> validIPs)
        {
            ValidIPs = validIPs;
        }

        public List<string> ValidIPs { get; }
    }
}
