using MediatR;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Vehicles.Commands
{
    public record UpdateVehicleCommand(Guid Id, string LicensePlate, string Model, string Make) : IRequest<Guid>;

    public class UpdateVehicleCommandHanlder : IRequestHandler<UpdateVehicleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateVehicleCommandHanlder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(request.Id);
            if (vehicle == null)
            {
                throw new Exception($"Vehicle with Id:{request.Id} not found");
            }
            vehicle.LicensePlate = request.LicensePlate;
            vehicle.Make = request.Make;
            vehicle.Model = request.Model;

            await _unitOfWork.Vehicles.UpdateAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return vehicle.Id;
        }
    }
}
