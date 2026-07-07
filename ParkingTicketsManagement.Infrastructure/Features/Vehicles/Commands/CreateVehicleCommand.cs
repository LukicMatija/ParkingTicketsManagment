using MediatR;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Vehicles.Commands
{
    public record CreateVehicleCommand(string LicensePlate, string Model, string Marka, Guid UserId) : IRequest<Guid>;

    public class CreateVehicleCommandHanlder : IRequestHandler<CreateVehicleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateVehicleCommandHanlder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var v = await _unitOfWork.Vehicles.GetAllAsync();
            if(v.Any(v => v.LicensePlate == request.LicensePlate))
            {
                throw new Exception("Licence plate exists");
            }
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                LicensePlate=request.LicensePlate,
                Make=request.LicensePlate,
                Model=request.Model,
                UserId=request.UserId
            };
            await _unitOfWork.Vehicles.AddAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return vehicle.Id;
        }
    }
}
