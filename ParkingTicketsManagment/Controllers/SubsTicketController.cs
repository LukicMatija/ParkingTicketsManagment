using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.DTOs.SubsTicketDTOs;
using ParkingTicketsManagement.Infrastructure.Features.SubscriptionTickets.Commands;

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
                return BadRequest(new { message = "Podaci za kreiranje karte nisu validni." });
            }

            try
            {
                var command = new AddSubscriptionTicketCommand(dto.VehicleId, dto.Longitude, dto.Latitude);
                var ticketId = await _mediator.Send(command);

                return Ok(new { id = ticketId, message = "Pretplatna karta je uspešno aktivirana!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
