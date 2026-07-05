using MediatR;
using ParkingTicketsManagement.Infrastructure.DTOs.ZoneDTOs;
using ParkingTicketsManagment.Domain.Repositories;
using ParkingTicketsManagment.Infrastructure.DTOs.ZoneDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Zones.Queries
{
    public record GetAllZonesQuery() : IRequest<List<ZoneDTO>>;
    public class GetAllZonesQueryHandler : IRequestHandler<GetAllZonesQuery, List<ZoneDTO>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllZonesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<List<ZoneDTO>> Handle(GetAllZonesQuery request, CancellationToken cancellationToken)
        {
            var zones = await _uow.Zones.GetAllAsync();

            return zones.Select(z => new ZoneDTO(
                z.Id,
                z.Name,
                z.PricePerHour,
                z.ZoneBoundaries != null
                    ? z.ZoneBoundaries.Coordinates.Select(c => new PolygonDto{Longitude = c.X, Latitude = c.Y}).ToList()
                    : new List<PolygonDto>()
            )).ToList();
        }
    }
}
