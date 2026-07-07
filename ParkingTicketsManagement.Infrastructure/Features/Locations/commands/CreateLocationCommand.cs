using MediatR;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Features.Locations.commands
{
    public record CreateLocationCommand(string StreetName, string City, double Latitude, double Longitude) : IRequest<Guid>;
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateLocationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var point = geometryFactory.CreatePoint(new Coordinate(request.Longitude, request.Latitude));
            Zone? z = await _unitOfWork.Zones.FindByPoint(request.Latitude, request.Longitude);
            if (z == null)
            {
                throw new Exception("Zone not found");
            }
            var location = new ParkingTicketsManagment.Domain.Domains.Location
            {
                Id = Guid.NewGuid(),
                StreetName = request.StreetName,
                City = request.City,
                ZoneId = z.Id,
                Coordinates = point
            };

            await _unitOfWork.Locations.AddAsync(location);
            await _unitOfWork.SaveChangesAsync();

            return location.Id;
        }
    }
}
