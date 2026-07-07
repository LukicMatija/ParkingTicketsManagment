using MediatR;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using ParkingTicketsManagment.Infrastructure.DTOs.ZoneDTOs;
using System;
using System.Collections.Generic;
using System.Text;



namespace ParkingTicketsManagement.Infrastructure.Features.Zones.Commands
{
    public record CreateZoneCommand(string Name, decimal PricePerHour, List<PolygonDto> Polygon) : IRequest<Guid>;
    public class CreateZoneCommandHandler : IRequestHandler<CreateZoneCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        public CreateZoneCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
        {
            Zone z = new Zone { Name = request.Name, PricePerHour = request.PricePerHour };
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var koordinate = request.Polygon
                .Select(t => new Coordinate(t.Longitude, t.Latitude))
                .ToArray();

            if (koordinate.Length > 0 && !koordinate.First().Equals2D(koordinate.Last()))
            {
                var zatvoreneKoordinate = new Coordinate[koordinate.Length + 1];
                koordinate.CopyTo(zatvoreneKoordinate, 0);
                zatvoreneKoordinate[zatvoreneKoordinate.Length - 1] = koordinate.First();
                koordinate = zatvoreneKoordinate;
            }

            var linearRing = geometryFactory.CreateLinearRing(koordinate);

            if (!linearRing.IsCCW)
            {
                koordinate = koordinate.Reverse().ToArray();
            }

            Polygon p = geometryFactory.CreatePolygon(koordinate);
            z.ZoneBoundaries = p;
            z.Id = Guid.NewGuid();
            await _uow.Zones.AddAsync(z);
            await _uow.SaveChangesAsync();
            return z.Id;
        }
    }
}
