using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.DTOs.ViolationTypeDTOs;
using ParkingTicketsManagement.Infrastructure.Features.ViolationTypes.commands;
using ParkingTicketsManagement.Infrastructure.Features.ViolationTypes.queries;

namespace ParkingTicketsManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ViolationTypeController(IMediator IMediator)
        {
            _mediator = IMediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllViolationTypesQuery());
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid Id)
        {
            var result = await _mediator.Send(new GetViolationTypeByIdQuery(Id));
            if (result == null)
            {
                return NotFound(new { message = "Tip prekršaja nije pronađen." });
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateViolationTypeDto dto)
        {
            var command = new CreateViolationTypeCommand(dto.Name, dto.Description, dto.Amount);
            var id = await _mediator.Send(command);
            return Ok(new { id, message = "Tip prekršaja kreiran." });
        }
    }
}
