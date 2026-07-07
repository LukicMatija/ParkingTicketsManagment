using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.DTOs.ParkingTicketDTOs;
using ParkingTicketsManagement.Infrastructure.Features.ParkingTickets.Commands;
using ParkingTicketsManagement.Infrastructure.Features.ParkingTickets.Query;
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
        [Authorize(Roles = "Worker")]
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

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetTicketsForVehicle([FromRoute] Guid vehicleId)
        {
            try
            {
                var query = new GetTicketsForVehicleQuery(vehicleId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
