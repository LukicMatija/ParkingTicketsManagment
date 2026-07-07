using Microsoft.EntityFrameworkCore;
using ParkingTicketsManagement.Infrastructure.Data;
using ParkingTicketsManagment.Domain.Domains;
using ParkingTicketsManagment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTicketsManagement.Infrastructure.Repositories
{
    internal class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Vehicle>> getAllVehiclesForUserAsync(Guid UserId)
        {
            return await DbSet.Where(v => v.UserId == UserId).ToListAsync();
        }
    }
}
