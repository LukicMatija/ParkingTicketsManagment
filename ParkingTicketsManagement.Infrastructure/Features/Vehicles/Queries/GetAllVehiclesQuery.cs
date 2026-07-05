using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.VehicleDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Vehicles.Queries
{
    public record GetAllVehiclesQuery() : IRequest<IEnumerable<VehicleResponseDTO>>;
    public class GetAllVehiclesQueryHanlder : IRequestHandler<GetAllVehiclesQuery, IEnumerable<VehicleResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllVehiclesQueryHanlder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<VehicleResponseDTO>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            return vehicles.Select(v => new VehicleResponseDTO
            {
                Id = v.Id,
                LicensePlate = v.LicensePlate,
                Make = v.Make,
                Model = v.Model,
                UserId = v.UserId
            });
        }

    }
}
