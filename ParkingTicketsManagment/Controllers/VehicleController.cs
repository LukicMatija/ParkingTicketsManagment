using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicketsManagement.Infrastructure.DTOs.VehicleDTOs;
using ParkingTicketsManagement.Infrastructure.Features.Vehicles.Commands;
using ParkingTicketsManagement.Infrastructure.Features.Vehicles.Queries;
using ParkingTicketsManagment.Domain.Repositories;

namespace ParkingTicketsManagment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VehicleController (IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllVehiclesQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid Id)
        {
            var result = await _mediator.Send(new GetVehicleByIdQuery(Id));
            if (result == null)
            {
                return NotFound(new { message = "Vehicle not found" });
            }
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchByLicensePlate([FromQuery] string licensePlate)
        {
            var result = await _mediator.Send(new GetVehicleByLicensePlateQuery(licensePlate));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateVehicleDTO dto)
        {
            var command = new CreateVehicleCommand(dto.LicensePlate, dto.Make, dto.Model, dto.UserId);
            var id = await _mediator.Send(command);
            return Ok(new { id, message = "Vozilo kreirano." });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateVehicleDTO dto)
        {
            try
            {
                var command = new UpdateVehicleCommand(id, dto.LicensePlate, dto.Model, dto.Make);
                var updatedId = await _mediator.Send(command);
                return Ok(new { id = updatedId, message = "Vehicle updateted." });
            }catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var deletedId = await _mediator.Send(new DeleteVehicleCommand(id));
                return Ok(new { id = deletedId, message = "Vozilo obrisano." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("my-vehicles")]
        public async Task<IActionResult> GetMyVehicles()
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new { message = "User isnt detected" });
                }

                var query = new GetVehiclesForUserQuery(userId);
                var vehicles = await _mediator.Send(query);

                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
