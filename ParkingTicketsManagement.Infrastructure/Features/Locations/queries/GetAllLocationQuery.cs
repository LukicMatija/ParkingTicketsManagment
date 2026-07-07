using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.LocationDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Locations.queries
{
    public record GetAllLocationQuery() : IRequest<IEnumerable<LocationResponseDto>>;
    public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, IEnumerable<LocationResponseDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetAllLocationQueryHandler(IUnitOfWork unitOfWork)
        {
            unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LocationResponseDto>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            var locations =await unitOfWork.Locations.GetAllAsync();
            return locations.Select(l => new LocationResponseDto
            {
                Id = l.Id,
                StreetName = l.StreetName,
                City = l.City,
                ZoneId = l.ZoneId,
                Latitude = l.Coordinates.Y,
                Longitude = l.Coordinates.X
            });
        }
    }
}
