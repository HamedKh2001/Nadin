using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nadin.Application.Features.UserFeature.Commands.CreateUser;
using Nadin.Application.Features.UserFeature.Commands.DeleteUser;
using Nadin.Application.Features.UserFeature.Commands.UpdateUser;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using Nadin.Application.Features.UserFeature.Queries.GetUsers;
using Nadin.WebAPI.Controllers;
using SharedKernel.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.WebAPI.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ApiControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedList<UserDto>>> Get([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Get([FromRoute] GetUserQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userDto = await Mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { userId = userDto.Id }, userDto);
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] long Id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            if (Id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] DeleteUserCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
