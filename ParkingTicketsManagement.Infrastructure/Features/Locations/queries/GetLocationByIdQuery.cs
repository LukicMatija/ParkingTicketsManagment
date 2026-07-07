using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.LocationDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Locations.queries
{
    public record GetLocationByIdQuery(Guid Id) : IRequest<LocationResponseDto?>;
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationResponseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetLocationByIdQueryHandler(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        public async Task<LocationResponseDto?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var Location = await _unitOfWork.Locations.GetByIdAsync(request.Id);
            if (Location == null)
            {
                return null;
            }
            return new LocationResponseDto
            {
                Id = Location.Id,
                City = Location.City,
                Latitude = Location.Coordinates.Y,
                Longitude = Location.Coordinates.X,
                StreetName = Location.StreetName,
                ZoneId = Location.ZoneId
            };
        }

    }
}
