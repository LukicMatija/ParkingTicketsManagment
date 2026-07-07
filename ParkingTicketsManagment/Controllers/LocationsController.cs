using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.DTOs.LocationDTOs;
using ParkingTicketsManagement.Infrastructure.Features.Locations.commands;
using ParkingTicketsManagement.Infrastructure.Features.Locations.queries;

namespace ParkingTicketsManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllLocationQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetLocationByIdQuery(id));
            if (result == null)
            {
                return NotFound(new { message = "Lokacija nije pronađena." });
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLocationDto dto)
        {
            var command = new CreateLocationCommand(dto.StreetName, dto.City, dto.Latitude, dto.Longitude);
            var id = await _mediator.Send(command);
            return Ok(new { id, message = "Lokacija kreirana." });
        }
    }
}
