using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.VehicleDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Vehicles.Queries
{
    public record GetVehicleByLicensePlateQuery(string LicensePlate) : IRequest<IEnumerable<VehicleResponseDTO>>;
    public class GetVehicleByLicensePlateQueryHandler : IRequestHandler<GetVehicleByLicensePlateQuery, IEnumerable<VehicleResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetVehicleByLicensePlateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<VehicleResponseDTO>> Handle(GetVehicleByLicensePlateQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.Vehicles.FindAsync(v => v.LicensePlate.Contains(request.LicensePlate));

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
