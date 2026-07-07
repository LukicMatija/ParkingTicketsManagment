using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.DTOs.ParkingTicketDTOs;
using ParkingTicketsManagement.Infrastructure.Features.ParkingTickets.Commands;
using System.Security.Claims;

namespace ParkingTicketsManagment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingTicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParkingTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IssueTicket([FromBody] AddParkingTicketDto dto)
        {
            try
            {
                var workerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(workerIdClaim) || !Guid.TryParse(workerIdClaim, out Guid workerId))
                {
                    return Unauthorized(new { message = "Worker authorization failed or token is invalid." });
                }

                var command = new IssueParkingTicketCommand(dto, workerId);
                var ticketId = await _mediator.Send(command);

                return Ok(new { id = ticketId, message = "Ticket successfully issued and processed." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
