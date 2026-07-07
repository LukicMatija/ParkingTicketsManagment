using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.VehicleDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Vehicles.Queries
{
    public record GetVehiclesForUserQuery(Guid UserId) : IRequest<List<VehicleResponseDTO>>;

    public class GetVehiclesForUserQueryHandler : IRequestHandler<GetVehiclesForUserQuery, List<VehicleResponseDTO>>
    {
        private readonly IUnitOfWork _uow;

        public GetVehiclesForUserQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<VehicleResponseDTO>> Handle(GetVehiclesForUserQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _uow.Vehicles.getAllVehiclesForUserAsync(request.UserId);

            return vehicles.Select(v => new VehicleResponseDTO
            {
                Id = v.Id,
                LicensePlate = v.LicensePlate,
                Make = v.Make,
                Model = v.Model,
                UserId = v.UserId
            }).ToList();
        }
    }
}
