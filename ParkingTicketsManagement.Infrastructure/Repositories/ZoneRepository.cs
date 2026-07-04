using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using ParkingTicketsManagement.Infrastructure.Data;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Repositories
{
    public class ZoneRepository : Repository<Zone>, IZoneRepository
    {
        public ZoneRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Zone?> FindByPoint(double latitude, double longitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            Point point = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
            return await DbSet.FirstOrDefaultAsync(z => z.ZoneBoundaries.Contains(point));
        }
    }
}
