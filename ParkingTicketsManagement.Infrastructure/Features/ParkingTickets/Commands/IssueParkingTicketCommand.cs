using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.ParkingTicketDTOs;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.ParkingTickets.Commands
{
    public record IssueParkingTicketCommand(AddParkingTicketDto Dto, Guid WorkerId) : IRequest<Guid>;
    public class IssueParkingTicketCommandHandler : IRequestHandler<IssueParkingTicketCommand, Guid>
    {
        private readonly IUnitOfWork _uow;

        public IssueParkingTicketCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> Handle(IssueParkingTicketCommand request, CancellationToken cancellationToken)
        {
            bool isAlreadyFined = await _uow.ParkingTickets.vehicleFined(request.Dto.VehicleId, request.Dto.ViolationTypeId);

            if (isAlreadyFined)
            {
                throw new Exception("Vehicle already fined in last 1 hour");
            }

            var workerExists = await _uow.Users.GetByIdAsync(request.WorkerId);
            if (workerExists == null)
            {
                throw new Exception("The worker with the provided ID does not exist in the system.");
            }

            var vehicleExists = await _uow.Vehicles.GetByIdAsync(request.Dto.VehicleId);
            if (vehicleExists == null)
            {
                throw new Exception("The vehicle with the provided ID does not exist in the system.");
            }

            var locationExists = await _uow.Locations.GetByIdAsync(request.Dto.LocationId);
            if (locationExists == null)
            {
                throw new Exception("The location with the provided ID does not exist in the system.");
            }

            var violationTypeExists = await _uow.ViolationTypes.GetByIdAsync(request.Dto.ViolationTypeId);
            if (violationTypeExists == null)
            {
                throw new Exception("The violation type with the provided ID does not exist in the system.");
            }

            var newTicket = new ParkingTicket
            {
                Id = Guid.NewGuid(),
                TicketNumber = request.Dto.TicketNumber,
                Amount = violationTypeExists.Amount,
                IssuedAt = DateTime.Now,
                Status = TicketStatus.Unpaid,
                VehicleId = request.Dto.VehicleId,
                LocationId = request.Dto.LocationId,
                ViolationTypeId = request.Dto.ViolationTypeId,
                WorkerId = request.WorkerId
            };

            await _uow.ParkingTickets.AddAsync(newTicket);
            await _uow.SaveChangesAsync();

            return newTicket.Id;
        }
    }
}
