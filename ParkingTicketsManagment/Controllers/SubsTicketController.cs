using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.DTOs.SubsTicketDTOs;
using ParkingTicketsManagement.Infrastructure.Features.SubscriptionTickets.Commands;
using ParkingTicketsManagement.Infrastructure.Features.SubscriptionTickets.Queries;

namespace ParkingTicketsManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubsTicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubsTicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSubscriptionTicket([FromBody] AddSubscriptionTicketDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Details is missing" });
            }

            try
            {
                var command = new AddSubscriptionTicketCommand(dto.VehicleId, dto.Longitude, dto.Latitude);
                var ticketId = await _mediator.Send(command);

                return Ok(new { id = ticketId, message = "Sub ticket bought" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("check-validity")]
        public async Task<IActionResult> CheckTicketValidity([FromQuery] Guid vehicleId, [FromQuery] Guid zoneId)
        {
            if (vehicleId == Guid.Empty || zoneId == Guid.Empty)
            {
                return BadRequest(new { message = "VehicleId i ZoneId missing" });
            }

            try
            {
                var query = new IsSubsTicketValidQuery(vehicleId, zoneId);
                bool isValid = await _mediator.Send(query);

                return Ok(new { isValid = isValid });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
