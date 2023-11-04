using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nadin.Application.Features.GroupFeature.Commands.CreateGroup;
using Nadin.Application.Features.GroupFeature.Commands.DeleteGroup;
using Nadin.Application.Features.GroupFeature.Commands.UpdateGroup;
using Nadin.Application.Features.GroupFeature.Commands.UpdateGroupRole;
using Nadin.Application.Features.GroupFeature.Commands.UpdateGroupUsers;
using Nadin.Application.Features.GroupFeature.Queries.GetGroup;
using Nadin.Application.Features.GroupFeature.Queries.GetGroupRoles;
using Nadin.Application.Features.GroupFeature.Queries.GetGroups;
using Nadin.Application.Features.GroupFeature.Queries.GetGroupUsers;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using Nadin.WebAPI.Controllers;
using SharedKernel.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.WebAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroupController : ApiControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<ActionResult<PaginatedList<GroupDto>>> Get([FromQuery] GetGroupsQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<ActionResult<GroupDto>> Get([FromRoute] GetGroupQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<IActionResult> Create([FromBody] CreateGroupCommand command, CancellationToken cancellationToken)
        {
            var group = await Mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { groupId = group.Id }, group);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<IActionResult> Update([FromRoute] long Id, [FromBody] UpdateGroupCommand command, CancellationToken cancellationToken)
        {
            if (Id != command.Id)
                return BadRequest();

            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<IActionResult> UpdateGroupRole([FromBody] UpdateGroupRoleCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGroupCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("[action]/{GroupId}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<ActionResult<GroupDto>> GetGroupRoles([FromRoute] GetGroupRolesQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetGroupUsers([FromQuery] GetGroupUsersQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPut("[action]/{Id}")]
        [Authorize(Roles = "SSO-Admin")]
        public async Task<IActionResult> UpdateGroupUsers([FromRoute] long Id, [FromBody] UpdateGroupUsersCommand command, CancellationToken cancellationToken)
        {
            if (Id != command.GroupId)
                return BadRequest();

            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
