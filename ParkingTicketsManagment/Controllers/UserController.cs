using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.Features.User.Queries;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Infrastructure.Features.User.Commands;

namespace ParkingTicketsManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeRole([FromRoute] Guid id, [FromQuery] Role newRole)
        {
            var command = new UpdateUserRoleCommand(id, newRole);

            try
            {
                if (!Enum.IsDefined(typeof(Role), newRole))
                {
                    return BadRequest(new { message = $"Role ({newRole}) doesnt exists" });
                }
                var updatedUserId = await _mediator.Send(command);

                return Ok(new
                {
                    userId = updatedUserId,
                    message = $"Role changed on {newRole}."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllUsersQuery();
                var users = await _mediator.Send(query);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
