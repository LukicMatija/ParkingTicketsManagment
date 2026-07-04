using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.Features.Zones.Commands;
using ParkingTicketsManagment.Infrastructure.DTOs.ZoneDTOs;

namespace ParkingTicketsManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ZoneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> createZone([FromBody] AddZoneDto zoneDto)
        {
            if (zoneDto == null)
            {
                return BadRequest(new { message = "Bad request" });
            }

            try
            {
                var command = new CreateZoneCommand(zoneDto.Name, zoneDto.PricePerHour, zoneDto.Polygons);

                var zoneId = await _mediator.Send(command);

                return Ok(new
                {
                    id = zoneId,
                    message = "Zone created"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
