using MediatR;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Vehicles.Commands
{
    public record DeleteVehicleCommand(Guid id) : IRequest<Guid>;
    public class DeleteVehicleCommandHanlder : IRequestHandler<DeleteVehicleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteVehicleCommandHanlder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(request.id);
            if (vehicle == null)
            {
                throw new Exception($"Vehicle with id{request.id} not found");
            }
            await _unitOfWork.Vehicles.RemoveAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return request.id;

        }
    }
}
