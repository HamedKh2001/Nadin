using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nadin.Application.Features.RoleFeature.Commands.UpdateRole;
using Nadin.Application.Features.RoleFeature.Queries.GetRoleGroups;
using Nadin.Application.Features.RoleFeature.Queries.GetRoles;
using Nadin.Application.Features.RoleFeature.Queries.GetRoleUsers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.WebAPI.Controllers.V1
{

    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoleController : ApiControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<ActionResult<List<RoleDto>>> Get(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetRolesQuery(), cancellationToken);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<IActionResult> Update([FromRoute] long Id, [FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            if (command.Id != Id)
                return BadRequest();

            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("[action]/{RoleId}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<ActionResult<List<GetRoleGroupsDto>>> GetRoleGroups([FromRoute] GetRoleGroupsQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("[action]/{RoleId}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<ActionResult<List<GetRoleUsersDto>>> GetRoleUsers([FromRoute] GetRoleUsersQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
    }
}
