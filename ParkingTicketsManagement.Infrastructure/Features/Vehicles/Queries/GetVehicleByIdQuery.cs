using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.VehicleDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Vehicles.Queries
{
    public record GetVehicleByIdQuery(Guid Id) : IRequest<VehicleResponseDTO?>;
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleResponseDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetVehicleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VehicleResponseDTO?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(request.Id);
            if (vehicle == null)
            {
                return null;
            }
            return new VehicleResponseDTO
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Make = vehicle.Make,
                Model = vehicle.Model,
                UserId=vehicle.UserId
            };
        }
    }
}
